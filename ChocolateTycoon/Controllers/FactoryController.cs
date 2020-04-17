using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using ChocolateTycoon.Services;
using ChocolateTycoon.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class FactoryController : Controller
    {
        private ApplicationDbContext db;

        public FactoryController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Factory
        public ActionResult Index(int? id)
        {
            var factories = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit);

            if (id != null)
                ViewBag.SelectedId = id.Value;

            ViewBag.Message = TempData["ErrorMessage"];
            ViewBag.MainStorageInfo = TempData["MainStorageInfo"];

            return View(factories);
        }

        // GET: Factory/Details/id
        public ActionResult Details(int? id)
        {
            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);

            if (factory == null)
                return HttpNotFound();

            var viewModel = new FactoryViewModel { Factory = factory };

            viewModel.GetEmployees();

            ViewBag.MainStorageInfo = TempData["MainStorageInfo"];

            return PartialView(viewModel);
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            var vault = db.Safes.Where(s => s.ID == 1).Single();

            if (vault == null)
                return HttpNotFound();

            if (!vault.MoneySuffice(Factory.CreateCost))
            {
                TempData["ErrorMessage"] = Message.ErrorMessage;
                return RedirectToAction("Index");
            }

            return View("FactoryForm");
        }

        // GET: Factory/Edit/id
        public ActionResult Edit(int? id)
        {
            var factory = db.Factories.SingleOrDefault(f => f.ID == id);

            if (factory == null)
                return HttpNotFound();

            var viewModel = new FactoryViewModel { Factory = factory };

            return View("FactoryForm", viewModel);
        }

        // POST: Factory/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Factory factory)
        {
            var factories = db.Factories;
            var vault = db.Safes.Where(s => s.ID == 1).Single();

            if (factory.ID == 0)
            {
                foreach (var f in factories)
                {
                    if (f.Name == factory.Name)
                    {
                        ModelState.AddModelError("Name", "This name already exists!");
                        break;
                    }
                }

                var newFactory = new Factory { Name = factory.Name };

                factories.Add(newFactory);
                vault.WithdrawAmount(Factory.CreateCost);
            }
            else
            {
                var factoryDb = db.Factories.SingleOrDefault(f => f.ID == factory.ID);
                factoryDb.Name = factory.Name;
            }

            if (!ModelState.IsValid)
                return View("FactoryForm");

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Factory/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);

            if (factory == null)
                return HttpNotFound();

            return View(factory);
        }

        // POST: Factory/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var vault = db.Safes.SingleOrDefault();

            var factoryToDelete = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);

            if (factoryToDelete == null)
                return RedirectToAction("Index");

            factoryToDelete.DoDelete(vault);

            db.Factories.Remove(factoryToDelete);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET/POST: /Factory/Produce
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Produce(int id)
        {
            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);

            var mainStorage = db.MainStorages.SingleOrDefault(m => m.ID == 1);

            if (factory.StorageUnit == null)
                Message.SetErrorMessage(MessageEnum.StorageUnitNullError);
            else
            {
                var chocolatesStored = db.Chocolates
                .Where(c => c.ChocolateStatusId == 2)
                .ToList();

                factory.Produce(mainStorage, chocolatesStored);

                db.Chocolates.AddRange(mainStorage.newProducts);

                TempData["MainStorageInfo"] = Message.MainStorageInfo;

                db.SaveChanges();
            }

            TempData["ErrorMessage"] = Message.ErrorMessage;

            return RedirectToAction("Index", new { id });
        }
    }
}