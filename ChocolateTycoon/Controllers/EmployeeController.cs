using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.ViewModels;

namespace ChocolateTycoon.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db;

        public EmployeeController()
        {
            db = new ApplicationDbContext();
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
            return View("EmployeeForm");
        }

        // GET: Employee/Edit
        public ActionResult Edit(int id)
        {
            var employee = db.Employees.SingleOrDefault(e => e.Id == id);

            if (employee == null)
                return HttpNotFound();

            ViewBag.Factories = new SelectList(db.Factories, "Id", "Name");

            return View("EmployeeForm", employee);
        }

        // POST: Employee/Save/Id?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee employee)
        {
            var employees = db.Employees.ToList();

            if (employee.Id == 0)
            {
                foreach (var item in employees)
                {
                    if (item.FullName == employee.FullName)
                    {
                        ModelState.AddModelError("Name", "This name already exists!");
                        break;
                    }
                }

                var newEmployee = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Position = employee.Position
                };

                db.Employees.Add(newEmployee);
            }
            else
            {
                var employeeDb = db.Employees.SingleOrDefault(e => e.Id == employee.Id);

                employeeDb.FirstName = employee.FirstName;
                employeeDb.LastName = employee.LastName;
                employeeDb.Position = employee.Position;
                employeeDb.Salary = employee.SetSalary(employee);
                employeeDb.FactoryID = employee.FactoryID;
            }

            if (!ModelState.IsValid)
                return View("EmployeeForm");

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}