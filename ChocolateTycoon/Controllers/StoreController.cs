using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using ChocolateTycoon.ViewModels;

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

        // GET: Store
        public ActionResult Index(int? id)
        {
            var stores = db.Stores;
                //.Include(s => s.Chocolates);

            if (id != null)
            {
                ViewBag.SelectedId = id.Value;
            }

            return View(stores);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var store = db.Stores
                //.Include(s => s.Chocolates)
                .SingleOrDefault(s => s.ID == id);

            if (store == null)
                return HttpNotFound();

            return PartialView("_Details", store);
        }

        public ActionResult New()
        {
            return View("StoreForm");
        }

        // GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var store = db.Stores.SingleOrDefault(s => s.ID == id);

            if (store == null)
                return HttpNotFound();

            return View("StoreForm", store);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Store store)
        {
            if (!ModelState.IsValid)
            {
                return View("StoreForm", store);
            }

            //Create
            if (store.ID == 0)
            {
                db.Stores.Add(store);
            }
            else // Update
            { 
                var storeInDb = db.Stores.Single(m => m.ID == store.ID);
                storeInDb.Name = store.Name;
                storeInDb.Level = store.Level;
            }

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
                //.Include(s => s.Chocolates)
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
                //.Include(s => s.Chocolates)
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

        // restock
        //public PartialViewResult Restock(Store store)
        //{
        //    var chocolatesOfMainStorage = db.MainStorages.Select(m => m.Chocolates);

        //    return PartialView("_Restock");
        //}
    }
}