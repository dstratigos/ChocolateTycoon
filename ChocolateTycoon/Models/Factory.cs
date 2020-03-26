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
        public bool PersonelSuffice(Factory factory)
        {
            var managersEmployed = factory.Employees.Where(e => e.Position == EmployeePosition.FactoryManager).Count();
            var employeesEmployed = factory.Employees.Where(e => e.Position == EmployeePosition.ProductionEngineer).Count();

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
            if(Supplier != null)
            {
                var supplierName = Supplier.Name;
                Supplier = null;

                return $"{supplierName} broke the Contract with {Name} Factory";
            }

            return "Something went wrong. Try again";
            
        }

        public string MakeContract(Supplier supplier)
        {
            if (Supplier != null && supplier.Id == Supplier.Id)                
                return "This Supplier already has a valid Contract with this Factory!";

            Supplier = supplier;
            return "A contract has been made with this Factory";
        }

    }
}