using ChocolateTycoon.Data;
using ChocolateTycoon.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class SafeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SafeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        
        // GET: Safe
        public ActionResult Vault()
        {
            var vault = unitOfWork.Safes.GetSafe();

            return PartialView("_VaultPartial", vault);
        }

        public ActionResult CheatDeposit()
        {
            var vault = unitOfWork.Safes.GetSafe();

            vault.CheatDepositAmount();

            unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CheatWithdraw()
        {
            var vault = unitOfWork.Safes.GetSafe();

            vault.CheatWithdrawAmount();

            unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }
    }
}