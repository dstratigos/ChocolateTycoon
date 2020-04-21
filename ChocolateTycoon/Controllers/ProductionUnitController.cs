using ChocolateTycoon.Models;
using ChocolateTycoon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.Data;
using ChocolateTycoon.Persistence;

namespace ChocolateTycoon.Controllers
{
    public class ProductionUnitController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductionUnitController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // POST: ProductionUnit/Create
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(Factory factory)
        {
            var vault = unitOfWork.Safes.GetSafe();

            if (!vault.MoneySuffice(ProductionUnit.CreateCost))
            {
                TempData["ErrorMessage"] = Message.ErrorMessage;
                return RedirectToAction("Index", "Factory", new { id = factory.ID });
            }

            ProductionUnit productionUnit = new ProductionUnit { FactoryID = factory.ID };

            unitOfWork.ProductionUnits.Add(productionUnit);
            vault.WithdrawAmount(ProductionUnit.CreateCost);
            unitOfWork.Complete();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }

    }
}