using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.Data;

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
            StorageUnit storageUnit = new StorageUnit
            {
                FactoryID = factory.ID
            };

            db.StorageUnits.Add(storageUnit);
            db.SaveChanges();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }
    }
}