using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
using System.Linq;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class TurnController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TurnController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult EndTurn()
        {
            var productionUnits = unitOfWork.ProductionUnits.GetProductionUnits().ToList();
            var employees = unitOfWork.Employees.GetEmployees().ToList();
            var stores = unitOfWork.Stores.GetStores().ToList();
            var safe = unitOfWork.Safes.GetSafe();

            var turn = new Turn(stores, productionUnits, employees, safe);

            if (productionUnits.Count() == 0)
                return HttpNotFound();

            turn.EndTurn();

            unitOfWork.Complete();

            return Content(Turn.TurnMessage);
        }
    }
}