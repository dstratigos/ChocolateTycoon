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
            var factories = db.Factories
                .Include(f => f.ProductionUnit)
                .Where(f => f.ProductionUnit != null)
                .ToList();

            if (factories.Count() == 0)
                return HttpNotFound();

            if(Turn.LooseEnds(factories))
                return View()





        }
    }
}