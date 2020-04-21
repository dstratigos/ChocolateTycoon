using ChocolateTycoon.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ChocolateTycoon.Core.ViewModels
{
    public class MainStorageViewModel
    {
        public MainStorage MainStorage { get; set; }        
        public IDictionary<string, int> availableStorage = new Dictionary<string, int>();


        public List<Chocolate> Chocolates { get; set; }
        public IDictionary<string, int> availableChocolates = new Dictionary<string, int>();

        public List<string> enumNames = GetChocolateDisplayNames();

        public MainStorageViewModel()
        {
            Chocolates = new List<Chocolate>();
        }

        private static List<string> GetChocolateDisplayNames()
        {
            var values = Enum.GetValues(typeof(ChocolateType));
            var names = new List<string>();

            foreach (var item in values)
            {
                names.Add(item.GetType()
                    .GetMember(item.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name);
            }

            return names;
        }

        public void GetChocolates()
        {
            var types = Enum.GetNames(typeof(ChocolateType)).ToList();

            foreach (var type in types)
            {
               availableChocolates.Add(type, Chocolates.Where(c => c.ChocolateType.ToString() == type).Count());
            }
        }

        public void GetStorage()
        {
            var types = Enum.GetNames(typeof(ChocolateType)).ToList();

            foreach (var type in types)
            {
                availableStorage.Add(type, MainStorage.maxPerShelf - availableChocolates[type]);
            }
        }
    }
}