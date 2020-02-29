using ChocolateTycoon.Models;
using ChocolateTycoon.Services;
using ChocolateTycoon.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Factory
        public ActionResult Index(int? id)
        {
            var factories = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit);

            if (id != null)
                ViewBag.SelectedId = id.Value;           

            return View(factories);
        }

        // GET: Factory/Details/id
        [ChildActionOnly]
        public PartialViewResult Details(int? id)
        {
            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .FirstOrDefault(f => f.ID == id);

            var chocolates = db.Chocolates
                .Include(c => c.Status)
                .ToList();

            ViewBag.ChocolateCount = FactoryService.PopulateChocolates(chocolates);
            ViewBag.ProductionError = TempData["ErrorMessage"];

            return PartialView(factory);
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            return View("FactoryForm");
        }

        // GET: Factory/Edit/id
        public ActionResult Edit(int? id)
        {
            var factory = db.Factories.SingleOrDefault(f => f.ID == id);

            if (factory == null)
                return HttpNotFound();

            return View("FactoryForm", factory);
        }

        // POST: Factory/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Factory factory)
        {
            var factories = db.Factories;

            if (factory.ID == 0)
            {
                foreach (var f in factories)
                {
                    if (f.Name == factory.Name)
                    {
                        ModelState.AddModelError("Name", "This name already exists!");
                        break;
                    }
                }

                var newFactory = new Factory
                {
                    Name = factory.Name,
                    ProductionUnit = new ProductionUnit { MaxProductionPerDay = 200 }
                };

                factories.Add(newFactory);
            }
            else
            {
                var factoryDb = db.Factories.SingleOrDefault(f => f.ID == factory.ID);
                factoryDb.Name = factory.Name;
            }

            if (!ModelState.IsValid)
                return View("FactoryForm");

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Factory/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .SingleOrDefault(f => f.ID == id);

            if (factory == null)
                return HttpNotFound();

            return View(factory);
        }

        // POST: Factory/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Factory factory)
        {
            var factories = db.Factories;

            var factoryToDelete = factories
                .Include(f => f.ProductionUnit)
                .SingleOrDefault(f => f.ID == factory.ID);

            if (factory == null)
                return HttpNotFound();

            if (factoryToDelete.ProductionUnit != null)
            {
                factoryToDelete.ProductionUnit = null;
            }

            factories.Remove(factoryToDelete);

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Factory employees by position
        public PartialViewResult FactoryEmployees(int? id)
        {
            var viewModel = new FactoryEmployeesViewModel
            {
                Factory = db.Factories.SingleOrDefault(f => f.ID == id),
                Employees = db.Employees.Where(e => e.FactoryID == id).ToList()
            };

            viewModel.GetEmployees();

            return PartialView("_FactoryEmployees", viewModel);
        }


        // GET: /Factory/Produce
        public ActionResult Produce(int id)
        {
            var factory = db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);

            TempData["ErrorMessage"] = FactoryService.Produce(factory);

            var chocolatesProduced = factory.StorageUnit._chocolates.ToList();

            foreach (var chocolate in chocolatesProduced)
            {
                db.Chocolates.Add(chocolate);
            }

            db.SaveChanges();

            return RedirectToAction("Index", new { id });
        }




    }
}