using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.ViewModels
{
    public class EmployeeFormViewModel
    {
        public IEnumerable<Factory> Factories { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public Employee Employee { get; set; }
        public string _errorMessage;
    }
}