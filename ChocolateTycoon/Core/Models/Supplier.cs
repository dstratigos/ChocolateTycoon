using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChocolateTycoon.Core.Models
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

        public List<Factory> Factories { get; set; }


        public Supplier()
        {
            Factories = new List<Factory>();
        }
    }
}