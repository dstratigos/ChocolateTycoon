using ChocolateTycoon.Core;
using ChocolateTycoon.Core.Models;
using System.Web.Mvc;

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
                var errorMessage = Message.ErrorMessage;
                return RedirectToAction("Index", "Factory", new { id = factory.ID, errorMessage });
            }

            ProductionUnit productionUnit = new ProductionUnit { FactoryID = factory.ID };

            unitOfWork.ProductionUnits.Add(productionUnit);
            vault.WithdrawAmount(ProductionUnit.CreateCost);
            unitOfWork.Complete();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }

    }
}