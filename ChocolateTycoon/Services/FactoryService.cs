using ChocolateTycoon.Models;
using ChocolateTycoon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Services
{
    public class FactoryService
    {
        public int PopulateChocolates(List<Chocolate> chocolates)
        {
            var count = chocolates.Where(c => c.ChocolateStatusId == 1).Count();

            return count;
        }

        public void Produce(Factory factory, MainStorage mainStorage)
        {
            var productionUnit = factory.ProductionUnit;
            var storageUnit = factory.StorageUnit;

            var materialsNeeded = productionUnit.MaterialsNeeded();
            var materialsSuffice = storageUnit.MaterialsSuffice(materialsNeeded);
            var personelSuffice = factory.PersonelSuffice();

            if (personelSuffice && materialsSuffice)
            {
                var products = productionUnit.DailyProduction();

                mainStorage.newProducts.AddRange(products);
            }
            else if (!personelSuffice)
                Message.SetErrorMessage(MessageEnum.PersonelError);
            else
                Message.SetErrorMessage(MessageEnum.RawMaterialsError);
        }

        
    }
}