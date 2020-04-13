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

namespace ChocolateTycoon.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext db;

        public StoreController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult SellChocolates(int id)
        {
            var chocolates = db.Chocolates
                .Include(c => c.Status)
                .Where(c => c.StoreId == id)
                .ToList();

            var store = db.Stores
                .Include(s => s.Employees)
                .Include(s => s.Chocolates)
                .Include(s => s.MainStorage)
                .Include(s => s.Safe)
                .Where(s => s.ID == id).SingleOrDefault();

            TempData["Message"] = store.Sell(chocolates);

            //var newChocolates = store.Chocolates.Where(c => c.StatusID == 3);

            db.SaveChanges();

            return RedirectToAction("Index", new { id });
        }

        // GET: Store
        public ActionResult Index(int? id)
        {
            var stores = db.Stores
                .Include(s => s.Safe);

            if (id != null)
            {
                ViewBag.SelectedId = id.Value;
            }

            return View(stores);
        }

        public ActionResult IndexTest(int? id)
        {
            var stores = db.Stores
                .Include(s => s.Safe);

            if (id != null)
            {
                ViewBag.SelectedId = id.Value;
            }

            return View(stores);
        }

        public ActionResult Details(int? id)
        {
            var store = db.Stores
                .Include(s => s.Chocolates)
                .SingleOrDefault(s => s.ID == id);

            if (store == null)
                return HttpNotFound();

            ViewBag.SellingProcess = TempData["Message"];

            return PartialView("_Details", store);
        }

        public ActionResult Create()
        {
            var viewModel = new StoreFormViewModel
            {
                Heading = "Add a Store"
            };

            return View("StoreForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var store = db.Stores
                .Single(s => s.ID == id);

            var chocolates = db.Chocolates.Where(c => c.Status.Id == 3 && c.StoreId == id).ToList();

            var viewModel = new StoreFormViewModel
            {
                ID = store.ID,
                Name = store.Name,
                Heading = "Edit a Store"
            };

            return View("StoreForm", viewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("StoreForm", viewModel);
            }

            var store = new Store
            {
                Name = viewModel.Name,
            };

            db.Stores.Add(store);
            db.SaveChanges();

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

            var store = db.Stores.Single(s => s.ID == viewModel.ID);
            store.Name = viewModel.Name;

            db.SaveChanges();

            return RedirectToAction("Index", "Store");
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Store store = db.Stores
                .SingleOrDefault(s => s.ID == id);

            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Store store = db.Stores
                .SingleOrDefault(s => s.ID == id);

            db.Stores.Remove(store);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Store employees by position
        public PartialViewResult StoreEmployees(int? id)
        {
            var viewModel = new StoreEmployeesViewModel
            {
                Store = db.Stores.SingleOrDefault(s => s.ID == id),
                Employees = db.Employees.Where(e => e.StoreID == id).ToList()
            };

            viewModel.GetEmployees();

            return PartialView("_StoreEmployees", viewModel);
        }
    }
}