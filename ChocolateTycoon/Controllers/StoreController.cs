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
        public ActionResult Index()
        {
            var stores = db.Stores.ToList();

            return View(stores);
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var store = db.Stores.SingleOrDefault(s => s.ID == id);

        //    if (store == null)
        //        return HttpNotFound();

        //    return View(store);
        //}

        public ActionResult New()
        {
            var chocolates = db.Chocolates.ToList();

            var viewModel = new StoreFormViewModel
            {
                Chocolates = chocolates
            };

            return View("StoreForm", viewModel);
        }

        // GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var store = db.Stores.SingleOrDefault(s => s.ID == id);

            if (store == null)
                return HttpNotFound();

            var viewModel = new StoreFormViewModel(store)
            {
                Chocolates = db.Chocolates.ToList()
            };

            return View("StoreForm", viewModel);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Store store)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new StoreFormViewModel
                {
                    Chocolates = db.Chocolates.ToList()
                };

                return View("StoreForm", viewModel);
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
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Store");
        }
    }
}