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
        public static void SortProducts(MainStorage mainStorage, List<Chocolate> chocolatesStored)
        {
            var types = Enum.GetNames(typeof(Models.ChocolateType)).ToList();
            var succeeded = 0;
            var failed = 0;

            foreach (var type in types)
            {
                var stored = chocolatesStored.Where(c => c.ChocolateType.ToString() == type).Count();
                var produced = mainStorage.newProducts.Where(c => c.ChocolateType.ToString() == type).ToList();

                if (produced.Count() > 0)
                {
                    foreach (var product in produced)
                    {
                        if (mainStorage.maxPerShelf - stored >= 1)
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

        public static void GetChocolates(MainStorageViewModel viewModel)
        {
            var types = Enum.GetNames(typeof(Models.ChocolateType)).ToList();

            foreach (var type in types)
            {
                viewModel.availableChocolates.Add(type, viewModel.Chocolates.Where(c => c.ChocolateType.ToString() == type).Count());
            }
        }

        public static void GetStorage(MainStorageViewModel viewModel)
        {
            var types = Enum.GetNames(typeof(Models.ChocolateType)).ToList();

            foreach (var type in types)
            {
                viewModel.availableStorage.Add(type, viewModel.MainStorage.maxPerShelf - viewModel.availableChocolates[type]);
            }
        }
    }
}