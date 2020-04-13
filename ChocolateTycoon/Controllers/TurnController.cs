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

            var turn = new Turn();

            //if (productionUnits.Count() == 0)
            //    return HttpNotFound();

            if (!Turn.LooseEnds(productionUnits))
            {
                Turn.EndTurn(productionUnits);
                db.SaveChanges();                
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}