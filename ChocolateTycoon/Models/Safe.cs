using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChocolateTycoon.Models
{
    public class Safe
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Deposit { get; set; }


        public bool MoneySuffice(decimal cost)
        {
            if (Deposit >= cost)
                return true;

            Message.SetErrorMessage(MessageEnum.NotEnoughMoneyError);

            return false;
        }        

        public void DepositAmount(decimal amount)
        {
            Deposit += amount;
        }

        public void WithdrawAmount(decimal amount)
        {
            Deposit -= amount;
        }

        // for testing purposes
        public void CheatDepositAmount()
        {
            Deposit += 1000;
        }

        // for testing purposes
        public void CheatWithdrawAmount()
        {
            Deposit -= 1000;
        }

        public void Refund(decimal createCost)
        {
            decimal refund = createCost * 0.2M; // 20% of factory price when deleting

            Deposit += refund;
        }

        public void ReplenishExpenses(Supplier supplier)
        {
            decimal shippedAmount = Convert.ToDecimal(supplier.ShippedAmount);
            Deposit -= shippedAmount * supplier.PricePerKilo;
        }

        public decimal CalculateTotalWages(List<Employee> employees)
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