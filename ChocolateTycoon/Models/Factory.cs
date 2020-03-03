using System;
using System.Collections.Generic;
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

        public ProductionUnit ProductionUnit { get; set; }
        public StorageUnit StorageUnit { get; set; }
        public ICollection<Employee> Employees { get; set; }


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

    }
}