using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Services
{
    public class FactoryService
    {
        public static int PopulateChocolates(List<Chocolate> chocolates)
        {
            var count = chocolates.Where(c => c.ChocolateStatusId == 1).Count();
            
            return count;
        }


        public static string Produce(Factory factory)
        {
            var productionUnit = factory.ProductionUnit;
            var storageUnit = factory.StorageUnit;


            var materialsNeeded = productionUnit.MaterialsNeeded();
            var materialsSuffice = storageUnit.MaterialsSuffice(materialsNeeded);
            var personelSuffice = factory.PersonelSuffice(factory);

            if (personelSuffice && materialsSuffice)
            {
                var products = productionUnit.DailyProduction();

                foreach (var chocolate in products)
                {
                    storageUnit._chocolates.Add(chocolate);
                }

                products.Clear();
            }
            else if (!personelSuffice)
                return "Not enough personel to start production!";
            else
                return "Not enough raw materials in Storage!";

            return "";
        }
    }
}