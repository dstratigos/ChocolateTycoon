using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.ViewModels
{
    public class EmployeeFormViewModel
    {
        public IEnumerable<Factory> Factories { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public Employee Employee { get; set; }
    }
}