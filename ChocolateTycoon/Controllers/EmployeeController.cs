using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
using ChocolateTycoon.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = unitOfWork.Employees
                .GetEmployeesWithStoresAndFactories()
                .ToList();

            return View(employees);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var viewModel = new EmployeeFormViewModel
            {
                Factories = unitOfWork.Factories.GetFactories().ToList(),
                Stores = unitOfWork.Stores.GetStores().ToList()
            };

            return View("EmployeeForm", viewModel);
        }

        // GET: Employee/Edit
        public ActionResult Edit(int id)
        {
            var factories = unitOfWork.Factories.GetFactories().ToList();
            var stores = unitOfWork.Stores.GetStores().ToList();
            var employee = unitOfWork.Employees.GetEmployee(id);

            if (employee == null)
                return HttpNotFound();

            var viewModel = new EmployeeFormViewModel
            {
                Factories = factories,
                Stores = stores,
                Employee = employee
            };

            return View("EmployeeForm", viewModel);
        }

        // POST: Employee/Save/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmployeeFormViewModel viewModel)
        {
            var employees = unitOfWork.Employees.GetEmployees().ToList();

            if (viewModel.Employee.Id == 0)
            {
                var newEmployee = new Employee
                {
                    FirstName = viewModel.Employee.FirstName,
                    LastName = viewModel.Employee.LastName,
                    Position = viewModel.Employee.Position,
                    Salary = viewModel.Employee.SetSalary(viewModel.Employee),
                    FactoryID = viewModel.Employee.FactoryID,
                    StoreID = viewModel.Employee.StoreID
                };

                newEmployee.Id = newEmployee.NameExists(employees);

                if (newEmployee.Id != 0)
                {
                    viewModel.Employee = newEmployee;
                    viewModel.Factories = unitOfWork.Factories.GetFactories().ToList();
                    viewModel.Stores = unitOfWork.Stores.GetStores().ToList();
                    ViewBag.Message = "This employee already exists! You are now in Edit Mode!";
                    return View("EmployeeForm", viewModel);
                }

                unitOfWork.Employees.Add(newEmployee);
            }
            else
            {
                var employeeDb = unitOfWork.Employees.GetEmployee(viewModel.Employee.Id);

                employeeDb.FirstName = viewModel.Employee.FirstName;
                employeeDb.LastName = viewModel.Employee.LastName;
                employeeDb.Position = viewModel.Employee.Position;
                employeeDb.Salary = viewModel.Employee.SetSalary(viewModel.Employee);
                employeeDb.FactoryID = viewModel.Employee.FactoryID;
                employeeDb.StoreID = viewModel.Employee.StoreID;
            }

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // POST: Employee/Delete/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var employee = unitOfWork.Employees.GetEmployee(id);

            if (employee == null)
                return HttpNotFound();

            employee.FactoryID = null;
            employee.StoreID = null;

            unitOfWork.Employees.Remove(employee);

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}