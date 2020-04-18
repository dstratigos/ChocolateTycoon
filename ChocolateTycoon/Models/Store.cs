using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Store
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Store Name should contain at least 3 charactes.")]
        public string Name { get; set; }

        public byte Level { get; private set; }

        private const int _createCost = 1500;
        public static int CreateCost => _createCost;

        private const int _maxStorageCapacity = 100;

        public bool CompletedDailySales { get; set; }

        public int Stock { get => AvailableStock(); }

        public bool AdequateStaff { get => EnoughPersonnel(); }

        public bool AdequateChocolate { get => EnoughChocolates(); }

        public int MainStorageID { get; set; } = 1;

        public int SafeID { get; set; } = 1;

        public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Chocolate> Chocolates { get; private set; }

        public Store()
        {
            Level = 1;
            Employees = new List<Employee>();
            Chocolates = new List<Chocolate>();
        }

        public Store(string name)
        {
            Name = name;
            Level = 1;
            Employees = new List<Employee>();
            Chocolates = new List<Chocolate>();
        }

        public int AvailableStock()
        {
            var chocolatesInStock = Chocolates
                .Where(c => c.ChocolateStatusId == 3 && c.StoreId == ID)
                .ToList()
                .Count();

            return chocolatesInStock;
        }

        private bool EnoughPersonnel()
        {
            var managers = Employees.Where(e => e.Position == EmployeePosition.StoreManager).Count();
            var employees = Employees.Where(e => e.Position == EmployeePosition.SalesExpert).Count();

            if (Level == 1 || Level == 2)
            {
                if (managers >= 1 && employees >= 2)
                    return true;
            }
            else if (Level > 2)
            {
                if (managers >= 1 && employees >= 3)
                    return true;
            }

            return false;
        }

        public bool EnoughChocolates()
        {
            var count = Chocolates.Where(c => c.StoreId == ID && c.ChocolateStatusId == 3).Count();

            if (count == _maxStorageCapacity)
                return true;
            else
                return false;
        }

        public void Sell(List<Chocolate> chocolates)
        {
            if (AdequateStaff && AdequateChocolate)
            {
                foreach (var chocolate in chocolates)
                {
                    Safe.Deposit = Earnings(chocolates);
                    chocolate.MarkAsSold();
                }
                CompletedDailySales = true;
                //return $"Done! New Safe Deposit {Safe.Deposit}.";
            }
            //else if (!EnoughChocolates())
            //    return "Not enough chocolate stock. Please restock";
            //else if (!AdequateStaff)
            //    return "Not enough employees.";
            //else
            //    return "Something went wrong.";
        }

        private decimal Earnings(List<Chocolate> chocolates)
        {
            foreach (var chocolate in chocolates)
                Safe.DepositAmount(chocolate.Price);

            return Safe.Deposit;
        }

        private void Pricing(List<Chocolate> chocolates)
        {
            foreach (var chocolate in chocolates)
            {
                if (chocolate.ChocolateType == ChocolateType.Dark)
                    chocolate.Price = 5;
                else if (chocolate.ChocolateType == ChocolateType.AlmondMilk || chocolate.ChocolateType == ChocolateType.HazelnutMilk)
                    chocolate.Price = 4.5M;
                else if (chocolate.ChocolateType == ChocolateType.Milk)
                    chocolate.Price = 4;
                else
                    chocolate.Price = 4;
            }
        }

        public void Order(List<Chocolate> chocolates)
        {
            if (AdequateStaff && !AdequateChocolate)
            {
                for (int i = 0; i < _maxStorageCapacity; i++)
                {
                    foreach (var chocolate in chocolates)
                    {
                        chocolate.ToStore(ID);
                    }
                }

                Pricing(chocolates);

                //return "Restock completed";
            }
            //else if (EnoughChocolates())
            //    return "Your stock is already full!";
            //else
            //    return "You don't have enough employees yet...";
        }
    }
}