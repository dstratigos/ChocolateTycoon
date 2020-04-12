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
        //[ChildActionOnly]
        public PartialViewResult Details(int? id)
        {
            var factoryService = new FactoryService();

            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier)
                .Include(f => f.Employees)
                .FirstOrDefault(f => f.ID == id);

            var chocolates = db.Chocolates
                .Include(c => c.Status)
                .ToList();

            ViewBag.ChocolateCount = factoryService.PopulateChocolates(chocolates);
            //ViewBag.ProductionError = TempData["ErrorMessage"];
            //ViewBag.ProductionSuccess = TempData["SuccessMessage"];
            ViewBag.MainStorageInfo = TempData["MainStorageInfo"];

            return PartialView(factory);
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            var vault = db.Safes.Where(s => s.ID == 1).Single();

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

            return View("FactoryForm", factory);
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
                vault.Deposit -= Factory.CreateCost;
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
        public ActionResult DeletePost(Factory factory)
        {
            if (factory == null)
                return RedirectToAction("Index");

            var factories = db.Factories;

            var factoryToDelete = factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == factory.ID);

            if (factoryToDelete == null)
                return RedirectToAction("Index");

            if (factoryToDelete.ProductionUnit != null)
                factoryToDelete.ProductionUnit = null;

            if (factoryToDelete.StorageUnit != null)
                factoryToDelete.StorageUnit = null;

            if (factoryToDelete.Employees.Count() > 0)
                factoryToDelete.Employees.Clear();


            factories.Remove(factoryToDelete);

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Factory employees by position
        public PartialViewResult FactoryEmployees(int? id)
        {
            var viewModel = new FactoryViewModel
            {
                Factory = db.Factories.SingleOrDefault(f => f.ID == id),
                Employees = db.Employees.Where(e => e.FactoryID == id).ToList()
            };

            viewModel.GetEmployees();

            return PartialView("_FactoryEmployees", viewModel);
        }


        // GET/POST: /Factory/Produce
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Produce(int id)
        {
            var factoryService = new FactoryService();

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

                factory.Produce(mainStorage);

                MainStorageService.SortProducts(mainStorage, chocolatesStored);

                var chocolatesProduced = mainStorage.newProducts;

                foreach (var chocolate in chocolatesProduced)
                    db.Chocolates.Add(chocolate);

                TempData["MainStorageInfo"] = Message.MainStorageInfo;

                db.SaveChanges();
            }
            TempData["ErrorMessage"] = Message.ErrorMessage;

            return RedirectToAction("Index", new { id });
        }
    }
}