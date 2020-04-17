using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
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
        private readonly ApplicationDbContext db;
        private readonly UnitOfWork unitOfWork;

        public FactoryController()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(db);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Factory
        public ActionResult Index(int? id)
        {
            var factories = unitOfWork.Factories.GetFactoriesWithProductionAndStorageUnit();

            if (id != null)
                ViewBag.SelectedId = id.Value;

            ViewBag.Message = TempData["ErrorMessage"];
            ViewBag.MainStorageInfo = TempData["MainStorageInfo"];

            return View(factories);
        }

        // GET: Factory/Details/id
        public ActionResult Details(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryAllInclusive(id);

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
            var vault = unitOfWork.Safes.GetSafe();

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
        public ActionResult Edit(int id)
        {
            var factory = unitOfWork.Factories.GetFactory(id);

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
            var factories = unitOfWork.Factories.GetFactories().ToList();
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

                unitOfWork.Factories.Add(newFactory);
                vault.WithdrawAmount(Factory.CreateCost);
            }
            else
            {
                var factoryDb = unitOfWork.Factories.GetFactory(factory.ID);
                factoryDb.Name = factory.Name;
            }

            if (!ModelState.IsValid)
                return View("FactoryForm");

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // GET: Factory/Delete/id
        public ActionResult Delete(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryMinusSupplier(id);

            if (factory == null)
                return HttpNotFound();

            return View(factory);
        }

        // POST: Factory/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var vault = unitOfWork.Safes.GetSafe();

            var factoryToDelete = unitOfWork.Factories.GetFactoryMinusSupplier(id);

            if (factoryToDelete == null)
                return RedirectToAction("Index");

            factoryToDelete.DoDelete(vault);

            unitOfWork.Factories.Remove(factoryToDelete);

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // GET/POST: /Factory/Produce
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Produce(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryMinusSupplier(id);

            var mainStorage = unitOfWork.MainStorage.GetMainStorage();

            if (factory.StorageUnit == null)
                Message.SetErrorMessage(MessageEnum.StorageUnitNullError);
            else
            {
                var chocolatesStored = unitOfWork.Chocolates.GetMainStorageChocolates().ToList();

                factory.Produce(mainStorage, chocolatesStored);

                unitOfWork.Chocolates.Add(mainStorage.newProducts);

                TempData["MainStorageInfo"] = Message.MainStorageInfo;

                unitOfWork.Complete();
            }

            TempData["ErrorMessage"] = Message.ErrorMessage;

            return RedirectToAction("Index", new { id });
        }
    }
}