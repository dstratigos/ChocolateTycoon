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
        public ActionResult Index()
        {
            var factories = db.Factories
                .Include(f => f.ProductionUnit);

            return View(factories);
        }

        // GET: Factory/Edit/5
        public ActionResult Edit(int? id)
        {
            var factory = db.Factories.SingleOrDefault(f => f.ID == id);

            if (id == null)
                return HttpNotFound();

            return View(factory);
        }

        // POST: Factory/Edit/5
        [HttpPost]
        public ActionResult Save(Factory factory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factory).State = EntityState.Modified;                

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Factory");
        }
    }
}