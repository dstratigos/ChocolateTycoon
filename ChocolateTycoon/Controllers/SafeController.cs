using ChocolateTycoon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class SafeController : Controller
    {
        private ApplicationDbContext db;

        public SafeController()
        {
            db = new ApplicationDbContext();
        }

        
        // GET: Safe
        public ActionResult Vault()
        {
            var vault = db.Safes.Where(s => s.ID == 1).Single();

            return PartialView("_VaultPartial", vault);
        }

        //public ActionResult Deposit()
        //{
        //    var vault = db.Safes.Where(s => s.ID == 1).Single();

        //    vault.depositAmount();

        //    db.SaveChanges();

        //    return RedirectToAction("Index", "Factory");
        //}

        //public ActionResult Withdraw()
        //{
        //    var vault = db.Safes.Where(s => s.ID == 1).Single();

        //    vault.withdrawAmount();

        //    db.SaveChanges();

        //    return RedirectToAction("Index", "Factory");
        //}

        public ActionResult CheatDeposit()
        {
            var vault = db.Safes.Where(s => s.ID == 1).Single();

            vault.cheatDepositAmount();

            db.SaveChanges();

            return RedirectToAction("Index", "Factory");
        }

        public ActionResult CheatWithdraw()
        {
            var vault = db.Safes.Where(s => s.ID == 1).Single();

            vault.cheatWithdrawAmount();

            db.SaveChanges();

            return RedirectToAction("Index", "Factory");
        }
    }
}