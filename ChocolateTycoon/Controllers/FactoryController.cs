using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
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
            var factories = db.Factories;



            return View(factories);
        }
    }
}