using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
using ChocolateTycoon.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class FactoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public FactoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Factory
        public ActionResult Index(int? id, string errorMessage, string infoMessage)
        {
            var factories = unitOfWork.Factories.GetFactoriesWithProductionAndStorageUnit();

            if (factories == null)
                return HttpNotFound();

            var viewModel = new FactoryViewModel
            {
                Factories = factories,
                _errorMessage = errorMessage,
                _infoMessage = infoMessage
            };

            if (id != null)
                ViewBag.SelectedId = id.Value;

            //ViewBag.Message = TempData["ErrorMessage"];
            //ViewBag.MainStorageInfo = TempData["MainStorageInfo"];

            return View(viewModel);
        }

        // GET: Factory/Details/id
        public ActionResult Details(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryAllInclusive(id);

            if (factory == null)
                return HttpNotFound();

            var viewModel = new FactoryViewModel { Factory = factory };

            viewModel.GetEmployees();

            return PartialView(viewModel);
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            var vault = unitOfWork.Safes.GetSafe();

            if (vault == null)
                return HttpNotFound();

            if (!vault.MoneySuffice(Factory.CreateCost))
            {
                //TempData["ErrorMessage"] = Message.ErrorMessage;
                var errorMessage = Message.ErrorMessage;
                return RedirectToAction("Index", new { errorMessage } );
            }

            return View("FactoryForm");
        }

        // GET: Factory/Edit/id
        public ActionResult Edit(int id)
        {
            var factory = unitOfWork.Factories.GetFactory(id);

            if (factory == null)
                return HttpNotFound();

            var viewModel = new FactoryViewModel { Factory = factory };

            return View("FactoryForm", viewModel);
        }

        // POST: Factory/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Factory factory)
        {
            var factories = unitOfWork.Factories.GetFactories().ToList();
            var vault = unitOfWork.Safes.GetSafe();

            if (factory.ID == 0)
            {
                var newFactory = new Factory { Name = factory.Name };
                newFactory.ID = newFactory.NameExists(factories);

                if (newFactory.ID != 0)
                {
                    Message.SetErrorMessage(MessageEnum.FactoryExistsError);
                    var errorMessage = Message.ErrorMessage;
                    var viewModel = new FactoryViewModel { Factory = newFactory, _errorMessage = errorMessage };
                    //ViewBag.Message = "This Factory already exists! You are now in Edit Mode!";                    
                    return View("FactoryForm", viewModel);
                }

                unitOfWork.Factories.Add(newFactory);
                vault.WithdrawAmount(Factory.CreateCost);
            }
            else
            {
                var factoryDb = unitOfWork.Factories.GetFactory(factory.ID);
                factoryDb.Name = factory.Name;
            }

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // GET: Factory/Delete/id
        public ActionResult Delete(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryMinusSupplier(id);

            if (factory == null)
                return HttpNotFound();

            return View(factory);
        }

        // POST: Factory/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var vault = unitOfWork.Safes.GetSafe();

            var factoryToDelete = unitOfWork.Factories.GetFactoryMinusSupplier(id);

            if (factoryToDelete == null)
                return RedirectToAction("Index");

            factoryToDelete.DoDelete(vault);

            unitOfWork.Factories.Remove(factoryToDelete);

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // GET/POST: /Factory/Produce
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Produce(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryMinusSupplier(id);
            var mainStorage = unitOfWork.MainStorages.GetMainStorage();
            string errorMessage;
            string infoMessage = "";

            if (factory.StorageUnit == null)
                Message.SetErrorMessage(MessageEnum.StorageUnitNullError);
            else
            {
                var chocolatesStored = unitOfWork.Chocolates.GetMainStorageChocolates().ToList();

                factory.Produce(mainStorage, chocolatesStored);

                unitOfWork.Chocolates.Add(mainStorage.newProducts);

                //TempData["MainStorageInfo"] = Message.MainStorageInfo;
                infoMessage = Message.MainStorageInfo;

                unitOfWork.Complete();
            }

            //TempData["ErrorMessage"] = Message.ErrorMessage;
            errorMessage = Message.ErrorMessage;
           
            return RedirectToAction("Index", new { id, errorMessage, infoMessage });
        }
    }
}