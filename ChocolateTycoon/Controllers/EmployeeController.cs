using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.ViewModels;
using System.Net;
using ChocolateTycoon.Data;

namespace ChocolateTycoon.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db;

        public EmployeeController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }


        // GET: Employee
        public ActionResult Index()
        {
            var employees = db.Employees
                .Include(e => e.Factory)
                .Include(e => e.Store);

            return View(employees.ToList());
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var factories = db.Factories.ToList();
            var stores = db.Stores.ToList();

            var viewModel = new EmployeeFormViewModel
            {
                Factories = factories,
                Stores = stores
            };

            return View("EmployeeForm", viewModel);
        }

        // GET: Employee/Edit
        public ActionResult Edit(int id)
        {
            var factories = db.Factories.ToList();
            var stores = db.Stores.ToList();
            var employee = db.Employees.SingleOrDefault(e => e.Id == id);

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

        // POST: Employee/Save/Id?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmployeeFormViewModel viewModel)
        {
            var employees = db.Employees.ToList();

            if (viewModel.Employee.Id == 0)
            {
                foreach (var item in employees)
                {
                    if (item.FullName == viewModel.Employee.FullName)
                    {
                        ModelState.AddModelError("Name", "This name already exists!");
                        break;
                    }
                }

                var newEmployee = new Employee
                {
                    FirstName = viewModel.Employee.FirstName,
                    LastName = viewModel.Employee.LastName,
                    Position = viewModel.Employee.Position,
                    Salary = viewModel.Employee.SetSalary(viewModel.Employee),
                    FactoryID = viewModel.Employee.FactoryID,
                    StoreID = viewModel.Employee.StoreID
                };

                db.Employees.Add(newEmployee);
            }
            else
            {
                var employeeDb = db.Employees.SingleOrDefault(e => e.Id == viewModel.Employee.Id);

                employeeDb.FirstName = viewModel.Employee.FirstName;
                employeeDb.LastName = viewModel.Employee.LastName;
                employeeDb.Position = viewModel.Employee.Position;
                employeeDb.Salary = viewModel.Employee.SetSalary(viewModel.Employee);
                employeeDb.FactoryID = viewModel.Employee.FactoryID;
                employeeDb.StoreID = viewModel.Employee.StoreID;
            }

            if (!ModelState.IsValid)
                return View("EmployeeForm");

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Employee/Delete/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var employee = db.Employees
                .SingleOrDefault(e => e.Id == id);

            if (employee == null)
                return HttpNotFound();

            employee.FactoryID = null;
            employee.StoreID = null;

            db.Employees.Remove(employee);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}