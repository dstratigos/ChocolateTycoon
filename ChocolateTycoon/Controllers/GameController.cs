using ChocolateTycoon.Core;
using ChocolateTycoon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class GameController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public GameController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult ResetGame()
        {
            var stores = unitOfWork.Stores.GetStoresWithAllDetails().ToList();
            var factories = unitOfWork.Factories.GetFactoriesWithProductionAndStorageUnit().ToList();
            var chocolates = unitOfWork.Chocolates.GetChocolates().ToList();
            var employees = unitOfWork.Employees.GetEmployees().ToList();
            var defaultEmployees = Employee.Default();
            var vault = unitOfWork.Safes.GetSafe();

            if (stores.Count != 0)
                unitOfWork.Stores.RemoveMany(stores);

            if (factories.Count != 0)
                foreach (var factory in factories)
                {
                    factory.DoDelete(vault);
                    unitOfWork.Factories.Remove(factory);
                }

            if (chocolates.Count != 0)
                unitOfWork.Chocolates.RemoveMany(chocolates);

            if (employees.Count != 0)
                unitOfWork.Employees.RemoveMany(employees);

            vault.WithdrawAmount(vault.Deposit);
            vault.DepositAmount(6000);

            unitOfWork.Employees.AddMany(defaultEmployees);

            unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }
    }
}