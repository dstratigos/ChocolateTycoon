using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class FactoryController : Controller
    {
        private ApplicationDbContext db;

        public FactoryController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Factory
        public ActionResult Index(int? id)
        {
            var factories = db.Factories
                .Include(f => f.ProductionUnit);

            if (id != null)
            {
                ViewBag.SelectedId = id.Value;
            }

            return View(factories);
        }

        // GET: Factory/Details/5
        [ChildActionOnly]
        public PartialViewResult Details(int? id)
        {
            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .FirstOrDefault(f => f.ID == id);

            return PartialView(factory);
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Factory/Create
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(string name)
        {
            var factories = db.Factories;
            
            var factory = new Factory
            {
                Name = name,
                Level = 1,
                ProductionUnit = new ProductionUnit {MaxProductionPerDay=200 }
            };

            //foreach (var f in factories)
            //{
            //    if (f.Name == factory.Name)
            //    {
            //        ModelState.AddModelError("Name", "This name already exists!");
            //    }
            //}

            if (ModelState.IsValid)
            {
                factories.Add(factory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(factory);
        }




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }


    }
}