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


        //public string SortChocolates(List<Chocolate> chocolatesStored)
        //{
        //    var darkStored = chocolatesStored.Where(c => c.ChocolateType == Type.Dark).ToList();
        //    var milkStored = chocolatesStored.Where(c => c.ChocolateType == Type.Milk).ToList();
        //    var hazelStored = chocolatesStored.Where(c => c.ChocolateType == Type.HazelnutMilk).ToList();
        //    var almondStored = chocolatesStored.Where(c => c.ChocolateType == Type.AlmondMilk).ToList();
        //    var whiteStored = chocolatesStored.Where(c => c.ChocolateType == Type.White).ToList();
            
        //    var newDark = newProducts.Where(c => c.ChocolateType == Type.Dark).ToList();
        //    var newMilk = newProducts.Where(c => c.ChocolateType == Type.Milk).ToList();
        //    var newHazel = newProducts.Where(c => c.ChocolateType == Type.HazelnutMilk).ToList();
        //    var newAlmond = newProducts.Where(c => c.ChocolateType == Type.AlmondMilk).ToList();
        //    var newWhite = newProducts.Where(c => c.ChocolateType == Type.White).ToList();

        //    var succeeded = 0;
        //    var failed = 0;

        //    foreach (var chocolate in newDark)
        //    {
        //        if (maxPerShelf - darkStored.Count() >= 1)
        //        {
        //            darkStored.Add(chocolate);
        //            chocolate.ChocolateStatusId = 2;
        //            succeeded++;
        //        }
        //        else
        //        {
        //            chocolate.ChocolateStatusId = 5;
        //            failed++;
        //        }
        //    }

        //    foreach (var chocolate in newMilk)
        //    {
        //        if (maxPerShelf - milkStored.Count() >= 1)
        //        {
        //            milkStored.Add(chocolate);
        //            chocolate.ChocolateStatusId = 2;
        //            succeeded++;
        //        }
        //        else
        //        {
        //            chocolate.ChocolateStatusId = 5;
        //            failed++;
        //        }
        //    }

        //    foreach (var chocolate in newHazel)
        //    {
        //        if (maxPerShelf - hazelStored.Count() >= 1)
        //        {
        //            hazelStored.Add(chocolate);
        //            chocolate.ChocolateStatusId = 2;
        //            succeeded++;
        //        }
        //        else
        //        {
        //            chocolate.ChocolateStatusId = 5;
        //            failed++;
        //        }
        //    }

        //    foreach (var chocolate in newAlmond)
        //    {
        //        if (maxPerShelf - almondStored.Count() >= 1)
        //        {
        //            almondStored.Add(chocolate);
        //            chocolate.ChocolateStatusId = 2;
        //            succeeded++;
        //        }
        //        else
        //        {
        //            chocolate.ChocolateStatusId = 5;
        //            failed++;
        //        }
        //    }

        //    foreach (var chocolate in newWhite)
        //    {
        //        if (maxPerShelf - whiteStored.Count() >= 1)
        //        {
        //            whiteStored.Add(chocolate);
        //            chocolate.ChocolateStatusId = 2;
        //            succeeded++;
        //        }
        //        else
        //        {
        //            chocolate.ChocolateStatusId = 5;
        //            failed++;
        //        }
        //    }

        //    return $"{succeeded} chocolates stored, {failed} chocolates given to charity!";
        //}


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