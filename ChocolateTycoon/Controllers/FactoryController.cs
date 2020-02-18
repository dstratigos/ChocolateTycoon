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
            return View("FactoryForm");
        }

        // GET: factory/Edit
        public ActionResult Edit(int? id)
        {
            var factory = db.Factories.SingleOrDefault(f => f.ID == id);

            if (factory == null)
                return HttpNotFound();

            return View("FactoryForm", factory);
        }

        // POST: Factory/Create
        [HttpPost]
        public ActionResult Save(Factory factory)
        {
            var factories = db.Factories;
                        
            if (factory.ID == 0)
            {
                var newFactory = new Factory
                {
                    Name = factory.Name,
                    Level = 1,
                    ProductionUnit = new ProductionUnit { MaxProductionPerDay = 200 }
                };

                //foreach (var f in factories)
                //{
                //    if (f.Name == factory.Name)
                //    {
                //        ModelState.AddModelError("Name", "This name already exists!");
                //    }
                //}
                
                factories.Add(newFactory);
            }
            else
            {
                var factoryDb = db.Factories.SingleOrDefault(f => f.ID == factory.ID);
                factoryDb.Name = factory.Name;
            }



            if (ModelState.IsValid)
                db.SaveChanges();

            return RedirectToAction("Index");
        }





        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }


    }
}