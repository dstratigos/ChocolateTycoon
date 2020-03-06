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

        private readonly int maxStorage = 1000;
        public int MaxStorage => maxStorage;
        
        public int maxPerShelf;

        public List<Chocolate> newProducts;

        public MainStorage()
        {
            maxPerShelf = maxStorage / Enum.GetNames(typeof(Type)).Length;
            newProducts = new List<Chocolate>();
        }


        public string SortChocolates(List<Chocolate> chocolatesStored)
        {
            var darkStored = chocolatesStored.Where(c => c.ChocolateType == Type.Dark).ToList();
            
            var newDark = newProducts.Where(c => c.ChocolateType == Type.Dark).ToList();

            var succeed = 0;
            var failed = 0;

            foreach (var chocolate in newDark)
            {
                if (maxPerShelf - darkStored.Count() >= 1)
                {
                    chocolate.ChocolateStatusId = 2;
                    succeed++;
                }
                else
                {
                    chocolate.ChocolateStatusId = 5;
                    failed++;
                }
            }

            return $"{succeed} chocolates stored, {failed} chocolates given to charity!";
        }





    }
}