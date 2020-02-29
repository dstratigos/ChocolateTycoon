using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class StorageUnit
    {
        [Key]
        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "No materials left!")]
        public double RawMaterialAmount { get; set; }
        //public int _productsStored;
        public ICollection<Chocolate> _chocolates;



        public Factory Factory { get; set; }
        

        public StorageUnit()
        {
            _chocolates = new List<Chocolate>();
        }

        public bool MaterialsSuffice(double materialsNeeded)
        {
            if (materialsNeeded <= RawMaterialAmount)
                return true;

            return false;
        }
    }
}