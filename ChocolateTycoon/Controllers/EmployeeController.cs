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
    }
}