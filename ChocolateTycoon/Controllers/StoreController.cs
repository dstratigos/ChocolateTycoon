using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        public ActionResult Details(int? id)
        {
            var store = db.Stores.SingleOrDefault(s => s.ID == id);

            if (store == null)
                return HttpNotFound();

            return View(store);
        }
    }
}