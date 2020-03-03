using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.ViewModels
{
    public class FactoryEmployeesViewModel
    {
        public Factory Factory { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public int _total;
        public int _managers;
        public int _production;


        public void GetEmployees()
        {
            var total = Employees.Count();
            var managers = Employees.Where(e => e.Position == EmployeePosition.FactoryManager).Count();
            var production = Employees.Where(e => e.Position == EmployeePosition.ProductionEngineer).Count();

            _total = total;
            _managers = managers;
            _production = production;
        }
    }
}