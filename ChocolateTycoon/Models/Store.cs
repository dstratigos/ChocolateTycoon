using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Store
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public byte Level { get; set; }

        //public int MainStorageID { get; } = 1;

        public int SafeID { get; } = 1;

        //public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

        //public List<Chocolate> Chocolates { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}