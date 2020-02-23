using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.DTOs
{
    public class StorageUnitDTO
    {
        [Required]
        public int FactoryID { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "No materials left!")]
        public double RawMaterialAmount { get; set; }
        
        //public int _productsStored;


        public FactoryDTO FactoryDTO { get; set; }
        //public ICollection<ChocolateDTO> ChocolatesDTO { get; set; }

        
    }
}