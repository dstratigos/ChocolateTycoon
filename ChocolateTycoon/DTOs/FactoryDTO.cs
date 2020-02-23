using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.DTOs
{
    public class FactoryDTO
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Please enter at least 3 characters")]
        public string Name { get; set; }
        public byte Level { get; private set; }

        public ProductionUnitDTO ProductionUnitDTO { get; set; }
        public StorageUnitDTO StorageUnitDTO { get; set; }
    }
}