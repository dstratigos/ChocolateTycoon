using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var stores = db.Stores;

            return View(stores);
        }
    }
}