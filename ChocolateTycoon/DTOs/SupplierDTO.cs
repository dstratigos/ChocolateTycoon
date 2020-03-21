using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int OfferAmount { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal PricePerKilo { get; set; }

        public int ShippedAmount { get; set; }

        public ICollection<SuppliedFactoryDto> Factories { get; set; }

        public SupplierDto()
        {
            Factories = new Collection<SuppliedFactoryDto>();
        }
    }
}