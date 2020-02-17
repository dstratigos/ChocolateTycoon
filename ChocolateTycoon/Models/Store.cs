using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Store
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte Level { get; set; }

        public int MainStorageID { get; set; }

        public int SafeID { get; set; }

        public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

        public List<Chocolate> Chocolates { get; set; }
    }
}