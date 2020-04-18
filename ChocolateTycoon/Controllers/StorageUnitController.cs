using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.Data;
using ChocolateTycoon.ViewModels;
using ChocolateTycoon.Persistence;

namespace ChocolateTycoon.Controllers
{
    public class StorageUnitController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UnitOfWork unitOfWork;

        public StorageUnitController()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(db);
        }

        // POST: /factory/details/id
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(Factory factory)
        {
            var vault = unitOfWork.Safes.GetSafe();

            if (!vault.MoneySuffice(StorageUnit.CreateCost))
            {
                TempData["ErrorMessage"] = Message.ErrorMessage;
                return RedirectToAction("Index", "Factory", new { id = factory.ID });
            }
            
            StorageUnit storageUnit = new StorageUnit { FactoryID = factory.ID };

            unitOfWork.StorageUnits.Add(storageUnit);
            vault.WithdrawAmount(StorageUnit.CreateCost);
            unitOfWork.Complete();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }

        [HttpPost]
        public ActionResult Replenish(int id)
        {
            var factories = unitOfWork.Factories.GetFactoriesWithStorageUnitAndSupplier().ToList();
            var factory = factories.Where(f => f.ID == id).Single();
            var safe = db.Safes.SingleOrDefault();

            factory.StorageUnit.Replenish(factories, factory.Supplier, safe);

            unitOfWork.Complete();

            TempData["ErrorMessage"] = Message.ErrorMessage;

            return RedirectToAction("Index", "Factory", new { id });
        }
    }
}