using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using ChocolateTycoon.ViewModels;
using ChocolateTycoon.Data;
using ChocolateTycoon.Persistence;

namespace ChocolateTycoon.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UnitOfWork unitOfWork;

        public StoreController()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(db);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult SellChocolates(int id)
        {
            var chocolates = unitOfWork.Chocolates.GetChocolatesWithStatus(id).ToList();

            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            store.Sell(chocolates);

            unitOfWork.Complete();

            return View("Index", new { id });
        }

        public ActionResult Restock(int id)
        {
            var chocolates = unitOfWork.Chocolates.GetChocolatesOfMainStorage().ToList();

            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            store.Order(chocolates);

            unitOfWork.Complete();

            return View("Index", new { id });
        }

        // GET: Store
        public ActionResult Index(int? id)
        {
            var stores = unitOfWork.Stores.GetStoresWithSafe();

            if (id != null)
                ViewBag.SelectedId = id.Value;

            return View(stores);
        }

        public ActionResult Details(int id)
        {
            var store = unitOfWork.Stores.GetStoreWithChocolates(id);

            if (store == null)
                return HttpNotFound();

            //ViewBag.SellingProcess = TempData["Message"];

            return PartialView("_Details", store);
        }

        public ActionResult Create()
        {
            var safe = unitOfWork.Safes.GetSafe();

            var viewModel = new StoreFormViewModel
            {
                Heading = "Add a Store"
            };

            if (!safe.MoneySuffice(Store.CreateCost))
            {
                TempData["ErrorMessage"] = Message.ErrorMessage;
                return RedirectToAction("Index");
            }

            return View("StoreForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var store = unitOfWork.Stores.GetStore(id);

            var viewModel = new StoreFormViewModel(store)
            {
                Heading = "Edit a Store"
            };

            return View("StoreForm", viewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreFormViewModel viewModel)
        {
            var stores = unitOfWork.Stores.GetStoresWithSafe();

            if (!ModelState.IsValid)
            {
                return View("StoreForm", viewModel);
            }

            var store = new Store
            {
                Name = viewModel.Name,
                Safe = stores.Select(s => s.Safe).FirstOrDefault()
            };

            unitOfWork.Stores.Add(store);
            //store.Safe.Deposit -= Store.CreateCost;
            store.Safe.WithdrawAmount(Store.CreateCost);

            unitOfWork.Complete();

            return RedirectToAction("Index", "Store");
        }

        // POST: Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(StoreFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("StoreForm", viewModel);
            }

            var store = unitOfWork.Stores.GetStore(viewModel.ID);
            store.Name = viewModel.Name;

            unitOfWork.Complete();

            return RedirectToAction("Index", "Store");
        }

        // GET: Delete
        public ActionResult Delete(int id)
        {
            var store = unitOfWork.Stores.GetStore(id);

            if (store == null)
            {
                return HttpNotFound();
            }

            return View(store);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var store = unitOfWork.Stores.GetStore(id);

            if (store == null)
                return HttpNotFound();

            unitOfWork.Stores.Remove(store);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // GET: Store employees by position
        public PartialViewResult StoreEmployees(int id)
        {
            var viewModel = new StoreEmployeesViewModel
            {
                Store = unitOfWork.Stores.GetStore(id),
                Employees = unitOfWork.Employees.GetEmployeesBasedOnStore(id).ToList()
            };

            viewModel.GetEmployees();

            return PartialView("_StoreEmployees", viewModel);
        }
    }
}