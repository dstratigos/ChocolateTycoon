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
        [StringLength(100)]
        public string Name { get; set; }

        public byte Level { get; private set; }

        private const int _createCost = 1500;
        public static int CreateCost => _createCost;

        private const int _maxStorageCapacity = 100;

        public bool CompletedDailySales { get; set; }

        public int MainStorageID { get; set; } = 1;

        public int SafeID { get; set; } = 1;

        public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

        public string ImageBase64 { get; set; }

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
            var chocolatesInStock = Chocolates.Where(c => c.ChocolateStatusId == 3).ToList().Count();

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

            bool enoughChocolates = true;

            if (count > 1)
                return enoughChocolates;
            else
                return !enoughChocolates;
        }

        public string Sell(List<Chocolate> chocolates)
        {
            var personnel = EnoughPersonnel();
            var stock = EnoughChocolates();

            if (personnel && stock)
            {
                foreach (var chocolate in chocolates)
                {
                    Safe.Deposit = Earnings(chocolates);
                    chocolate.MarkAsSold();
                }
                CompletedDailySales = true;
                return $"Done! New Safe Deposit {Safe.Deposit}";
            }
            else if (!EnoughChocolates())
                return "Not enough chocolate stock";
            else if (!EnoughPersonnel())
                return "Not enough employees";
            else
                return "Something went wrong";
        }

        private decimal Earnings(List<Chocolate> chocolates)
        {
            foreach (var chocolate in chocolates)
                Safe.Deposit += chocolate.Price;

            return Safe.Deposit;
        }

        public void Order(List<Chocolate> chocolates) // * After every round if chocolates sold there sould be a new order
        {
            List<Chocolate> chocolatesMainStorage = new List<Chocolate>();

            if (!EnoughChocolates())
            {
                for (int i = 0; i < _maxStorageCapacity; i++)
                {
                     foreach (var chocolate in chocolates)
                    {
                        if (chocolate.ChocolateStatusId == 2)
                            chocolatesMainStorage.Add(chocolate);
                    }
                }
            }

            chocolatesMainStorage.ForEach(c => Chocolates.Add(c));
        }
    }
}