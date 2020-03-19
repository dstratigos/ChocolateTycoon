using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.ViewModels
{
    public class SupplierViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}