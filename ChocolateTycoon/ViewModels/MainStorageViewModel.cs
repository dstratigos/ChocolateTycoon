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
        //public int[] availableStorage = new int[Enum.GetValues(typeof(Models.Type)).Length + 1];

        public List<Chocolate> Chocolates { get; set; }
        //public int dark;
        //public int white;
        //public int milk;
        //public int almonds;
        //public int hazelnuts;
        public int[] availableChocolates = new int[Enum.GetValues(typeof(Models.Type)).Length];

        public MainStorageViewModel()
        {
            Chocolates = new List<Chocolate>();
        }

        public void GetChocolates()
        {
            //dark = Chocolates.Where(c => c.ChocolateType == Models.Type.Dark).Count();
            //white = Chocolates.Where(c => c.ChocolateType == Models.Type.White).Count();
            //milk = Chocolates.Where(c => c.ChocolateType == Models.Type.Milk).Count();
            //almonds = Chocolates.Where(c => c.ChocolateType == Models.Type.AlmondMilk).Count();
            //hazelnuts = Chocolates.Where(c => c.ChocolateType == Models.Type.HazelnutMilk).Count();

            var types = Enum.GetNames(typeof(Models.Type)).ToList();

            for (int i = 0; i < availableChocolates.Length; i++)
            {
                foreach (var type in types)
                {
                    availableChocolates[i] = Chocolates.Where(c => c.ChocolateType.ToString() == type).Count();
                    i++;
                }
            }
        }

        public void GetStorage()
        {
            //for (int i = 0; i < availableStorage.Length - 1; i++)
            //{
            //    availableStorage[i] = MainStorage.maxPerShelf - availableChocolates[i];
            //}

            //availableStorage[^1] = 3;
            darkAvailable = MainStorage.maxPerShelf - availableChocolates[0];
            milkAvailable = MainStorage.maxPerShelf - availableChocolates[1];
            almondsAvailable = MainStorage.maxPerShelf - availableChocolates[2];
            hazelnutsAvailable = MainStorage.maxPerShelf - availableChocolates[3];
            whiteAvailable = MainStorage.maxPerShelf - availableChocolates[4];
            totalAvailable = MainStorage.MaxStorage - Chocolates.Count();
        }
    }
}