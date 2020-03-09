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

        private readonly int maxStorage = 200 * Enum.GetNames(typeof(Type)).Length;
        public int MaxStorage => maxStorage;

        public int maxPerShelf;

        public List<Chocolate> newProducts;

        public MainStorage()
        {
            maxPerShelf = maxStorage / Enum.GetNames(typeof(Type)).Length;
            newProducts = new List<Chocolate>();
        }


        public string SortProducts(List<Chocolate> chocolatesStored)
        {
            var types = Enum.GetNames(typeof(Type)).ToList();

            var succeeded = 0;
            var failed = 0;

            foreach (var type in types)
            {
                var stored = chocolatesStored.Where(c => c.ChocolateType.ToString() == type).ToList();
                var produced = newProducts.Where(c => c.ChocolateType.ToString() == type).ToList();

                foreach (var product in produced)
                {
                    if (maxPerShelf - stored.Count() >= 1)
                    {
                        stored.Add(product);
                        product.ChocolateStatusId = 2;
                        succeeded++;
                    }
                    else
                    {
                        product.ChocolateStatusId = 5;
                        failed++;
                    }
                }
            }

            return $"{succeeded} chocolates stored, {failed} chocolates given to charity!";
        }
        
    }
}