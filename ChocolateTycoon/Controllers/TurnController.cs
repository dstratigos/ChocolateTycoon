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
            var safe = db.Safes.SingleOrDefault(s => s.ID == 1);

            var turn = new Turn(productionUnits, employees, safe);

            if (productionUnits.Count() == 0)
                return HttpNotFound();

            //if (!turn.LooseEnds(productionUnits))
            //{
            //    turn.EndTurn();
            //    db.SaveChanges();
            //}
            //Turn.TurnMessage = "Hello!";

            turn.EndTurn();

            db.SaveChanges();


            return Redirect(Request.UrlReferrer.PathAndQuery);
            //return View("EndTurn", turn);
        }
    }
}