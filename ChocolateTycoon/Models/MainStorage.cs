using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class MainStorage
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int maxStorage = 5000;
        public int maxByType;

        public MainStorage()
        {
            maxByType = maxStorage / 5;
        }

    }
}