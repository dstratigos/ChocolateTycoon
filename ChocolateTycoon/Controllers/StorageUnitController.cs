using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.Data;
using ChocolateTycoon.ViewModels;

namespace ChocolateTycoon.Controllers
{
    public class StorageUnitController : Controller
    {
        private ApplicationDbContext db;

        public StorageUnitController()
        {
            db = new ApplicationDbContext();
        }

        // POST: /factory/details/id
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(Factory factory)
        {
            var vault = db.Safes.SingleOrDefault(s => s.ID == 1);

            if (!vault.MoneySuffice(StorageUnit.CreateCost))
            {
                TempData["ErrorMessage"] = Message.ErrorMessage;
                return RedirectToAction("Index", "Factory", new { id = factory.ID });
            }
            
            StorageUnit storageUnit = new StorageUnit
            {
                FactoryID = factory.ID
            };            

            db.StorageUnits.Add(storageUnit);
            vault.WithdrawAmount(StorageUnit.CreateCost);
            db.SaveChanges();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }

        [HttpPost]
        public ActionResult Replenish(int id)
        {
            var factories = db.Factories
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier)
                .ToList();
            var factory = factories.Where(f => f.ID == id).Single();
            var safe = db.Safes.SingleOrDefault();

            factory.StorageUnit.Replenish(factories, factory.Supplier, safe);

            db.SaveChanges();

            TempData["ErrorMessage"] = Message.ErrorMessage;

            return RedirectToAction("Index", "Factory", new { id });
        }
    }
}