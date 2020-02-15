using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Factory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }

        public ProductionUnit ProductionUnit { get; set; }

    }
}