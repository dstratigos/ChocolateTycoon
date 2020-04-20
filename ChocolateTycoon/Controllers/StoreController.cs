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

        public ActionResult SellChocolates(int id, bool sold = false)
        {
            var chocolates = unitOfWork.Chocolates.GetStoreChocolates(id).ToList();

            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            store.Sell(chocolates);

            unitOfWork.Complete();

            return RedirectToAction("Index", new { id, sold = true });
        }

        public ActionResult Restock(int id, bool restocked = false)
        {
            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            var chocolates = unitOfWork.Chocolates.GetMainStorageChocolates().Take(Store._maxStorageCapacity).ToList();

            store.Order(chocolates);

            unitOfWork.Complete();

            return RedirectToAction("Index", new { id, restocked = true });
        }

        // GET: Store
        public ActionResult Index(int? id)
        {
            var stores = unitOfWork.Stores.GetStores();

            if (id != null)
                ViewBag.SelectedId = id.Value;

            return View(stores);
        }

        public ActionResult Details(int id)
        {
            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            if (store == null)
                return HttpNotFound();

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

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreFormViewModel viewModel)
        {
            var safe = unitOfWork.Safes.GetSafe();

            if (!ModelState.IsValid)
            {
                return View("StoreForm", viewModel);
            }

            var store = new Store
            {
                Name = viewModel.Name,
                Safe = safe
            };

            unitOfWork.Stores.Add(store);
            safe.WithdrawAmount(Store.CreateCost);

            unitOfWork.Complete();

            return RedirectToAction("Index", "Store");
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

        //// GET: Delete
        //public ActionResult Delete(int id)
        //{
        //    var store = unitOfWork.Stores.GetStore(id);

        //    if (store == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(store);
        //}

        //// POST: Delete
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var store = unitOfWork.Stores.GetStore(id);

        //    if (store == null)
        //        return HttpNotFound();

        //    unitOfWork.Stores.Remove(store);
        //    unitOfWork.Complete();

        //    return RedirectToAction("Index");
        //}

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