using ChocolateTycoon.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.ViewModels
{
    public class EmployeeFormViewModel
    {
        public IEnumerable<Factory> Factories { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public Employee Employee { get; set; }
    }
}