using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public enum EmployeePosition
    {
        Manager,
        Sales,
        Production
    }

    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name = "Fullname")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required]
        public EmployeePosition Position { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }



        public int? StoreID { get; set; }
        public int? FactoryID { get; set; }


        public Store Store { get; set; }
        public Factory Factory { get; set; }


        public Employee()
        {
            Salary = SetSalary(this);
        }

        private decimal SetSalary(Employee employee)
        {
            switch ((int)employee.Position)
            {
                case 0:
                    return 1500;                   

                case 1:
                    return 900;

                case 2:
                    return 750;

                default:
                    return 0;
            }
        }
    }
}