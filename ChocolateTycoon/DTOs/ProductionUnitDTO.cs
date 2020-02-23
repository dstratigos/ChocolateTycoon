using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.DTOs
{
    public class ProductionUnitDTO
    {
        [Required]
        public int FactoryID { get; set; }
        public int MaxProductionPerDay { get; set; }

        public FactoryDTO FactoryDTO { get; set; }
    }
}