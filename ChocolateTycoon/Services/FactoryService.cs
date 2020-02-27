using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Services
{
    public class FactoryService
    {
        public static string DailyProduction(Factory factory)
        {
            var productionUnit = factory.ProductionUnit;
            var storageUnit = factory.StorageUnit;


            var materialsNeeded = productionUnit.MaterialsNeeded();
            var materialsSuffice = storageUnit.MaterialsSuffice(materialsNeeded);
            var personelSuffice = factory.PersonelSuffice();

            if (personelSuffice && materialsSuffice)
            {
                var products = productionUnit.DailyProduction();

                foreach (var chocolate in products)
                {
                    storageUnit.Chocolates.Add(chocolate);
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