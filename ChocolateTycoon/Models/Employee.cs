using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ChocolateTycoon.Models
{
    public enum EmployeePosition
    {
        [Display(Name ="Factory Manager")]
        FactoryManager,

        [Display(Name = "Store Manager")]
        StoreManager,

        [Display(Name = "Sales Expert")]
        SalesExpert,

        [Display(Name = "Production Engineer")]
        ProductionEngineer
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
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }



        public int? StoreID { get; set; }
        public int? FactoryID { get; set; }


        public Store Store { get; set; }
        public Factory Factory { get; set; }


        public Employee()
        {
            Salary = SetSalary(this);
        }

        public decimal SetSalary(Employee employee)
        {
            switch ((int)employee.Position)
            {
                case 0:
                    return 1700;                   

                case 1:
                    return 1500;

                case 2:
                    return 900;

                case 3:
                    return 1000;

                default:
                    return 0;
            }
        }

        
    }
}