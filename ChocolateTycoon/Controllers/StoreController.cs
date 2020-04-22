using ChocolateTycoon.Core;
using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //}

        public ActionResult SellChocolates(int id, bool sold = false)
        {
            var chocolates = unitOfWork.Chocolates.GetStoreChocolates(id).ToList();

            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            store.Sell(chocolates);

            var message = Message.Notification;

            unitOfWork.Complete();

            return RedirectToAction("Index", new { id, sold = true, message });
        }

        public ActionResult Restock(int id, bool restocked = false)
        {
            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            var chocolates = unitOfWork.Chocolates
                .GetMainStorageChocolates()
                .OrderBy(c => Guid.NewGuid())
                .Take(Store._maxStorageCapacity)
                .ToList();

            if (chocolates.Count() == 0)
                return RedirectToAction("Index", new { id, restocked = false });

            store.Order(chocolates);

            unitOfWork.Complete();

            return RedirectToAction("Index", new { id, restocked = true });
        }

        // GET: Store
        public ActionResult Index(int? id, bool completed = false, string errorMessage = null)
        {
            var stores = unitOfWork.Stores.GetStores();

            var viewModel = new StoresViewModel
            {
                Stores = stores,
                ErrorMessage = errorMessage
            };

            if (id != null)
                ViewBag.SelectedId = id.Value;

            return View(viewModel);
        }

        public ActionResult Details(int id, string message)
        {
            var store = unitOfWork.Stores.GetStoreWithAllDetails(id);

            if (store == null)
                return HttpNotFound();

            var viewModel = new StoreFormViewModel(store) { _message = message };

            return PartialView("_Details", viewModel);
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
                var errorMessage = Message.ErrorMessage;
                return RedirectToAction("Index", new { errorMessage });
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
                Name = viewModel.Store.Name,
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
            store.Name = viewModel.Store.Name;

            unitOfWork.Complete();

            return RedirectToAction("Index", "Store");
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