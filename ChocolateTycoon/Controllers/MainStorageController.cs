using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
using ChocolateTycoon.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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