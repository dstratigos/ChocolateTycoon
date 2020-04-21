using ChocolateTycoon.Core;
using System.Linq;
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