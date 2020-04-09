using ChocolateTycoon.Helpers;
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
        public string _message;
        public Factory Factory { get; set; }



        public bool MaterialsSuffice(double materialsNeeded)
        {
            if (materialsNeeded <= RawMaterialAmount)
                return true;

            return false;
        }

        public void Replenish(List<Factory> factories, Supplier supplier)
        {
            if (Factory.HasActiveContract(factories, FactoryID))
            {
                if (ShipmentsReceived >= supplier.OfferAmount)
                {
                    Factory.BreakContract();
                    //Message.SetErrorMessage(MessageEnum.NoSupplierError);
                    //_message = Message.ErrorMessage;
                    Message.SetErrorMessage(MessageEnum.SupplierQuotaError);
                    return;
                }

                ShipmentsReceived += supplier.ShippedAmount;
                RawMaterialAmount += supplier.ShippedAmount;

                return;
            }

            //_message = PosisionEnumHelper.GetDisplayName(MessageEnum.NoSupplierError);
            Message.SetErrorMessage(MessageEnum.NoSupplierError);
        }

        public void ResetSupplier()
        {
            ShipmentsReceived = 0;
        }
    }
}