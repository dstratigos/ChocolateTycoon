using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.ViewModels;
using ChocolateTycoon.Services;
using ChocolateTycoon.Data;

namespace ChocolateTycoon.Controllers
{
    public class MainStorageController : Controller
    {
        private ApplicationDbContext db;

        public MainStorageController()
        {
            db = new ApplicationDbContext();
        }


        // GET: MainStorage
        public ActionResult Index()
        {
            var mainStorage = db.MainStorages.SingleOrDefault(m => m.ID == 1);

            if (mainStorage == null)
                return HttpNotFound();

            var chocolates = db.Chocolates
                .Include(c => c.Status)
                .Where(c => c.ChocolateStatusId == 2)
                .ToList();

            if (chocolates == null)
                chocolates = new List<Chocolate>();

            var viewModel = new MainStorageViewModel()
            {
                MainStorage = mainStorage,
                Chocolates = chocolates
            };

            MainStorageService.GetChocolates(viewModel);
            MainStorageService.GetStorage(viewModel);

            return View(viewModel);
        }
    }
}