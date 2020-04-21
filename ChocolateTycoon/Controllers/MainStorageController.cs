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
using ChocolateTycoon.Persistence;

namespace ChocolateTycoon.Controllers
{
    public class MainStorageController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MainStorageController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: MainStorage
        public ActionResult Index()
        {
            var mainStorage = unitOfWork.MainStorages.GetMainStorage();

            if (mainStorage == null)
                return HttpNotFound();

            var chocolates = unitOfWork.Chocolates.GetMainStorageChocolates().ToList();

            if (chocolates == null)
                chocolates = new List<Chocolate>();

            var viewModel = new MainStorageViewModel()
            {
                MainStorage = mainStorage,
                Chocolates = chocolates
            };

            viewModel.GetChocolates();
            viewModel.GetStorage();

            return View(viewModel);
        }
    }
}