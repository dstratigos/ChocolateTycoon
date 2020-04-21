using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocolateTycoon.Models
{
    public class StorageUnit
    {
        [Key]
        [ForeignKey("Factory")]
        public int FactoryID { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:#,0.00}", ApplyFormatInEditMode = true)]
        public double RawMaterialAmount { get; set; }
        public double ShipmentsReceived { get; private set; }
        public string _message;
        private const int _createCost = 750;
        public static int CreateCost => _createCost;
        public Factory Factory { get; set; }

        public bool MaterialsSuffice(double materialsNeeded)
        {
            if (materialsNeeded <= RawMaterialAmount)
                return true;

            return false;
        }

        public void Replenish(List<Factory> factories, Supplier supplier, Safe safe)
        {
            if (Factory.HasActiveContract(factories, FactoryID))
            {
                if(supplier.ShippedAmount * supplier.PricePerKilo >= safe.Deposit)
                {
                    Message.SetErrorMessage(MessageEnum.NotEnoughMoneyError);
                    return;
                }

                if (ShipmentsReceived >= supplier.OfferAmount)
                {
                    Factory.BreakContract(safe);            
                    Message.SetErrorMessage(MessageEnum.SupplierQuotaError);
                    return;
                }

                safe.ReplenishExpenses(supplier);
                ShipmentsReceived += supplier.ShippedAmount;
                RawMaterialAmount += supplier.ShippedAmount;
                Message.SetErrorMessage(null);
                return;
            }

            Message.SetErrorMessage(MessageEnum.NoSupplierError);
        }

        public void ResetSupplier()
        {
            ShipmentsReceived = 0;
        }
    }
}