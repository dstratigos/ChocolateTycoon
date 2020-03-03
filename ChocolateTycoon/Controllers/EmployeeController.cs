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
        // Todo: Implement

        // POST: Employee/Save/Id?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee employee)
        {
            if (employee == null)
                return HttpNotFound();

            var newEmployee = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position
            };

            db.Employees.Add(newEmployee);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}