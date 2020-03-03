using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ChocolateTycoon.Controllers
{
    public class StorageUnitController : Controller
    {
        private ApplicationDbContext db;

        public StorageUnitController()
        {
            db = new ApplicationDbContext();
        }


        //// GET: StorageUnit/Create
        //public ActionResult Create(int id)
        //{
        //    var factory = db.Factories
        //        .Include(f => f.StorageUnit)
        //        .SingleOrDefault(f => f.ID == id);           

        //    return View(factory);
        //}

        // POST: StorageUnit/Create
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(Factory factory)
        {
            StorageUnit storageUnit = new StorageUnit
            {
                FactoryID = factory.ID,
                RawMaterialAmount = 100
            };

            //db.Entry(factory).State = EntityState.Modified;
            db.StorageUnits.Add(storageUnit);
            db.SaveChanges();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }
    }
}