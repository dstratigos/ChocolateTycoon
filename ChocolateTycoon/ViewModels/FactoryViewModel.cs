using ChocolateTycoon.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChocolateTycoon.ViewModels
{
    public class FactoryViewModel
    {
        public Factory Factory { get; set; }
        public IEnumerable<Factory> Factories { get; set; }
        public string _errorMessage;
        public string _infoMessage;
        public int _managers;
        public int _production;

        public void GetEmployees()
        {
            var managers = Factory.Employees.Where(e => e.Position == EmployeePosition.FactoryManager).Count();
            var production = Factory.Employees.Where(e => e.Position == EmployeePosition.ProductionEngineer).Count();

            _managers = managers;
            _production = production;
        }
    }
}