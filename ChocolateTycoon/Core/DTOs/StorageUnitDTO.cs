using System.ComponentModel.DataAnnotations;

namespace ChocolateTycoon.Core.DTOs
{
    public class StorageUnitDto
    {
        [Required]
        public int FactoryID { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "No materials left!")]
        public double RawMaterialAmount { get; set; }     

        public FactoryDto FactoryDTO { get; set; }

        
    }
}