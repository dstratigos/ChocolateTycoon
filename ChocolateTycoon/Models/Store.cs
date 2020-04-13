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

        public int MainStorageID { get; set; } = 1;

        public int SafeID { get; set; } = 1;

        public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

        public string ImageBase64 { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Chocolate> Chocolates { get; private set; }

        public Store()
        {
            Level = 1;
            Employees = new Collection<Employee>();
            Chocolates = new Collection<Chocolate>();
        }

        public Store(string name)
        {
            Name = name;
            Level = 1;
            Employees = new Collection<Employee>();
            Chocolates = new Collection<Chocolate>();
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
                //chocolates.ForEach(c => c.MarkAsSold());

                foreach (var chocolate in chocolates)
                {
                    Safe.Deposit = Earnings(chocolates);
                    chocolate.MarkAsSold();
                }

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

        public void Order(MainStorage mainStorage)
        {
            if (!EnoughChocolates())
            {
                for (int i = 0; i < 100; i++)
                {
                    var chocolates = mainStorage.newProducts.ToList();
                    chocolates.ForEach(c => c.ToStore());
                    Chocolates = chocolates;
                }
            }
        }
    }
}