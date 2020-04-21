using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Supplier
        public ActionResult Index()
        {
            var suppliers = unitOfWork.Suppliers.GetSuppliers().ToList();
            
            return View(suppliers);
        }
    }
}