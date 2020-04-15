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

        public void SortProducts(List<Chocolate> chocolatesStored)
        {
            var types = Enum.GetNames(typeof(ChocolateType)).ToList();
            var succeeded = 0;
            var failed = 0;

            foreach (var type in types)
            {
                var stored = chocolatesStored.Where(c => c.ChocolateType.ToString() == type).Count();
                var produced = newProducts.Where(c => c.ChocolateType.ToString() == type).ToList();

                if (produced.Count() > 0)
                {
                    foreach (var product in produced)
                    {
                        if (maxPerShelf - stored >= 1)
                        {
                            product.ChocolateStatusId = 2;
                            stored++;
                            succeeded++;
                        }
                        else
                        {
                            product.ChocolateStatusId = 5;
                            failed++;
                        }
                    }
                }
            }

            if (succeeded > 0 || failed > 0)
            {
                Message.SetErrorMessage(null);

            }
            Message.SetMainStorageInfo(succeeded, failed);
        }        
    }
}
