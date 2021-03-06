﻿using System.ComponentModel.DataAnnotations;

namespace ChocolateTycoon.Core.DTOs
{
    public class ProductionUnitDto
    {
        [Required]
        public int FactoryID { get; set; }
        public int MaxProductionPerDay { get; set; }

        public FactoryDto FactoryDTO { get; set; }
    }
}