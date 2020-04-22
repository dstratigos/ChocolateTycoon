using ChocolateTycoon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Core.ViewModels
{
    public class StoresViewModel
    {
        public IEnumerable<Store> Stores { get; set; }
        public string ErrorMessage { get; set; }
    }
}