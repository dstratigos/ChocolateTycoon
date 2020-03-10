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

        private readonly int maxStorage = 200 * Enum.GetNames(typeof(ChocolateType)).Length;
        public int MaxStorage => maxStorage;

        public int maxPerShelf;

        public List<Chocolate> newProducts;

        public MainStorage()
        {
            maxPerShelf = maxStorage / Enum.GetNames(typeof(ChocolateType)).Length;
            newProducts = new List<Chocolate>();
        }
    }
}