using ChocolateTycoon.Models;
using ChocolateTycoon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class ProductionUnitController : Controller
    {
        ApplicationDbContext db;

        public ProductionUnitController()
        {
            db = new ApplicationDbContext();
        }
        
        // ????
        [ChildActionOnly]
        public PartialViewResult Produce(Factory factory)
        {
            var message = FactoryService.DailyProduction(factory);
            
            return PartialView("_ProductionResult", message);
        }
    }
}