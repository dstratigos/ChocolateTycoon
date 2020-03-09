using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ChocolateTycoon.Models;

namespace ChocolateTycoon.ViewModels
{
    public class MainStorageViewModel
    {
        public MainStorage MainStorage { get; set; }
        public int darkAvailable;
        public int whiteAvailable;
        public int milkAvailable;
        public int almondsAvailable;
        public int hazelnutsAvailable;
        public int totalAvailable;
        public IDictionary<string, int> availableStorage = new Dictionary<string, int>();


        public List<Chocolate> Chocolates { get; set; }
        public IDictionary<string, int> availableChocolates = new Dictionary<string, int>();

        public MainStorageViewModel()
        {
            Chocolates = new List<Chocolate>();
        }


        public void GetChocolates()
        {
           var types = Enum.GetNames(typeof(Models.Type)).ToList();

            foreach (var type in types)
            {
                availableChocolates.Add(type, Chocolates.Where(c => c.ChocolateType.ToString() == type).Count());
            }
        }

        public void GetStorage()
        {
            var types = Enum.GetNames(typeof(Models.Type)).ToList();

            foreach (var type in types)
            {
                availableStorage.Add(type, MainStorage.maxPerShelf - availableChocolates[type]);
            }
        }
    }
}