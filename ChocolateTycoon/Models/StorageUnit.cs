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
        
        [Range(0, double.MaxValue)]
        public double RawMaterialAmount { get; set; }
        public double ShipmentsReceived { get; private set; }
        public Factory Factory { get; set; }

        

        public bool MaterialsSuffice(double materialsNeeded)
        {
            if (materialsNeeded <= RawMaterialAmount)
                return true;

            return false;
        }

        public void Replenish(Supplier supplier)
        {
            ShipmentsReceived += supplier.ShippedAmount;
            RawMaterialAmount += supplier.ShippedAmount;
        }

        public void ResetSupplier()
        {
            ShipmentsReceived = 0;
        }
    }
}