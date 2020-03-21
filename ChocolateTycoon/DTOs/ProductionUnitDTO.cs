using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.DTOs
{
    public class ProductionUnitDto
    {
        [Required]
        public int FactoryID { get; set; }
        public int MaxProductionPerDay { get; set; }

        public FactoryDto FactoryDTO { get; set; }
    }
}