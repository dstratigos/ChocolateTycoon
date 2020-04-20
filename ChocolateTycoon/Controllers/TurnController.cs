using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class TurnController : Controller
    {
        private ApplicationDbContext db;

        public TurnController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult EndTurn()
        {
            var productionUnits = db.ProductionUnits.ToList();
            var employees = db.Employees.ToList();
            var stores = db.Stores.ToList();
            var safe = db.Safes.SingleOrDefault(s => s.ID == 1);

            var turn = new Turn(stores, productionUnits, employees, safe);

            if (productionUnits.Count() == 0)
                return HttpNotFound();

            turn.EndTurn();

            db.SaveChanges();

            return Content(Turn.TurnMessage);
        }
    }
}