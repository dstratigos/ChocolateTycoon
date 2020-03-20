using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int OfferAmount { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal PricePerKilo { get; set; }

        public int ShippedAmount { get; set; }

        //public int FactoryId { get; set; }

        //public Factory Factory { get; set; }











    }
}