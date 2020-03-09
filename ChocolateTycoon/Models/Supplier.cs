using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Supplier
    {
        public int MyProperty { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int OfferAmount { get; set; }

        [Required]
        public decimal PricePerKilo { get; set; }
    }
}