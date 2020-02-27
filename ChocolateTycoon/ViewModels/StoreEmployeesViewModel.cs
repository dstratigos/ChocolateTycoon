using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.ViewModels
{
    public class StoreEmployeesViewModel
    {
        public Store Store { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public int _total;
        public int _manager;
        public int _sales;


        public void GetEmployees()
        {
            var total = Employees.Count();
            var manager = Employees.Where(e => e.Position == EmployeePosition.Manager).Count();
            var sales = Employees.Where(e => e.Position == EmployeePosition.Sales).Count();

            _total = total;
            _manager = manager;
            _sales = sales;
        }
    }
}