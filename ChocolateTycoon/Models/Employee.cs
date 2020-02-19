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
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
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
        public EmployeePosition Position { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }



        public int? StoreID { get; set; }
        public int? FactoryID { get; set; }


        public Store Store { get; set; }
        public Factory Factory { get; set; }
    }
}