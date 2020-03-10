using ChocolateTycoon.Models;
using ChocolateTycoon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Services
{
    public class MainStorageService
    {
        public static string SortProducts(MainStorage mainStorage, List<Chocolate> chocolatesStored)
        {
            var types = Enum.GetNames(typeof(Models.Type)).ToList();

            var succeeded = 0;
            var failed = 0;

            foreach (var type in types)
            {
                var stored = chocolatesStored.Where(c => c.ChocolateType.ToString() == type).ToList();
                var produced = mainStorage.newProducts.Where(c => c.ChocolateType.ToString() == type).ToList();

                foreach (var product in produced)
                {
                    if (mainStorage.maxPerShelf - stored.Count() >= 1)
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

        public static void GetChocolates(MainStorageViewModel viewModel)
        {
            var types = Enum.GetNames(typeof(Models.Type)).ToList();

            foreach (var type in types)
            {
                viewModel.availableChocolates.Add(type, viewModel.Chocolates.Where(c => c.ChocolateType.ToString() == type).Count());
            }
        }

        public static void GetStorage(MainStorageViewModel viewModel)
        {
            var types = Enum.GetNames(typeof(Models.Type)).ToList();

            foreach (var type in types)
            {
                viewModel.availableStorage.Add(type, viewModel.MainStorage.maxPerShelf - viewModel.availableChocolates[type]);
            }
        }
    }
}