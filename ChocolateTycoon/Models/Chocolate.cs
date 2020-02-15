using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public enum Type
    {
        Dark = 1,
        Milk,
        AlmondMilk,
        HazelnutMilk,
        White
    }

    public class Chocolate
    {
        public int ID { get; set; }

        public Type ChocolateType { get; set; }

        public decimal Price { get; set; }
    }
}