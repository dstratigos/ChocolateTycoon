using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Safe
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Deposit { get; set; }


        public bool MoneySuffice(int cost)
        {
            if (Deposit >= cost)
                return true;

            Message.SetErrorMessage(MessageEnum.NotEnoughMoneyError);

            return false;
        }

        // for testing purposes
        public void depositAmount()
        {
            Deposit += 2000;
        }

        public void withdrawAmount()
        {
            Deposit -= 1000;
        }

        public void FactoryRefund()
        {
            var price = Factory.CreateCost;

            decimal refund = price * 0.2M; // 20% of factory price when deleting

            Deposit += refund;
        }

        public void ReplenishExpenses(Supplier supplier)
        {
            decimal shippedAmount = Convert.ToDecimal(supplier.ShippedAmount);
            Deposit += supplier.PricePerKilo * shippedAmount;
        }

        public void BreakContractPenalty(StorageUnit storage, Supplier supplier)
        {
            decimal shipments = Convert.ToDecimal(storage.ShipmentsReceived);
            decimal offerAmount = Convert.ToDecimal(supplier.OfferAmount);
            var remainder = shipments - offerAmount;
            Deposit += remainder * 0.5M;
        }
    }    
}