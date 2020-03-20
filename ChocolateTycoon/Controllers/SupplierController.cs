using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class SupplierController : Controller
    {
        private ApplicationDbContext db;

        public SupplierController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Supplier
        public ActionResult Index()
        {
            var suppliers = db.Suppliers.ToList();
            
            return View(suppliers);
        }
    }
}