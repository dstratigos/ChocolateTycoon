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

        public void StoreRefund()
        {
            var price = Store.CreateCost;

            decimal refund = price * 0.2M; // 20% of store price when deleting

            Deposit += refund;
        }

        public void ReplenishExpenses(Supplier supplier)
        {
            decimal shippedAmount = Convert.ToDecimal(supplier.ShippedAmount);
            Deposit -= shippedAmount * supplier.PricePerKilo;
        }

        public void BreakContractPenalty(StorageUnit storage, Supplier supplier)
        {
            decimal shipments = Convert.ToDecimal(storage.ShipmentsReceived);
            decimal offerAmount = Convert.ToDecimal(supplier.OfferAmount);
            var remainder = offerAmount - shipments;
            Deposit -= remainder * 0.5M;
        }

        public decimal calculateTotalWages(List<Employee> employees)
        {
            decimal total = 0;

            foreach (var employee in employees)
            {
                if (employee.FactoryID != null || employee.StoreID != null)
                    total += employee.Salary;
            }

            return total;
        }
    }    
}