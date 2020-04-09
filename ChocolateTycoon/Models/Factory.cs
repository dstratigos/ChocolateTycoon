using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Factory
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Please enter at least 3 characters")]
        public string Name { get; set; }
        public byte Level { get; private set; }
        const int _createCost = 2000;
        public int CreateCost => _createCost;

        public int? SupplierId { get; set; }

        public ProductionUnit ProductionUnit { get; set; }
        public StorageUnit StorageUnit { get; set; }
        public List<Employee> Employees { get; set; }
        public Supplier Supplier { get; set; }


        public Factory()
        {
            Level = 1;
            Employees = new List<Employee>();
        }

        //checks if the factory personel meets the required minimum for the factory to operate
        public bool PersonelSuffice()
        {
            var managersEmployed = Employees.Where(e => e.Position == EmployeePosition.FactoryManager).Count();
            var employeesEmployed = Employees.Where(e => e.Position == EmployeePosition.ProductionEngineer).Count();

            if (Level == 1)
            {
                if (managersEmployed >= 1 && employeesEmployed >= 2)
                    return true;
            }
            else if (Level == 2)
            {
                if (managersEmployed >= 2 && employeesEmployed >= 4)
                    return true;
            }

            return false;
        }

        public string BreakContract()
        {
            if (Supplier != null)
            {
                var supplierName = Supplier.Name;
                Supplier = null;
                StorageUnit.ResetSupplier();

                return $"{Name} Factory broke it's Contract with {supplierName}";
            }

            return "Something went wrong. Try again";
        }

        public string MakeContract(Supplier supplier)
        {
            Supplier = supplier;
            //StorageUnit.Replenish(supplier);

            return $"A Contract has been made between {supplier.Name} and {Name} Factory.";
                   //+ $" <br/> First Load ({supplier.ShippedAmount} Kg) has been shipped.";
        }

        public static bool HasActiveContract(List<Factory> factories, int factoryId)
        {
            var suppliedFactories = factories.Where(f => f.Supplier != null).ToList();
            var hasSupplier = suppliedFactories.Exists(f => f.ID == factoryId);

            if (suppliedFactories != null)
                if (hasSupplier)
                    return true;

            return false;
        }
    }
}