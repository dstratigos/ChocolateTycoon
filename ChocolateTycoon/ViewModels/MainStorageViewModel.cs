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

        public List<Chocolate> Chocolates { get; set; }
        public int dark;
        public int white;
        public int milk;
        public int almonds;
        public int hazelnuts;

        public MainStorageViewModel()
        {
            Chocolates = new List<Chocolate>();
        }

        public void GetChocolates()
        {
            dark = Chocolates.Where(c => c.ChocolateType == Models.Type.Dark).Count();
            white = Chocolates.Where(c => c.ChocolateType == Models.Type.White).Count();
            milk = Chocolates.Where(c => c.ChocolateType == Models.Type.Milk).Count();
            almonds = Chocolates.Where(c => c.ChocolateType == Models.Type.AlmondMilk).Count();
            hazelnuts = Chocolates.Where(c => c.ChocolateType == Models.Type.HazelnutMilk).Count();
        }

        public void GetStorage()
        {
            darkAvailable = MainStorage.maxByType - dark;
            whiteAvailable = MainStorage.maxByType - white;
            milkAvailable = MainStorage.maxByType - milk;
            almondsAvailable = MainStorage.maxByType - almonds;
            hazelnutsAvailable = MainStorage.maxByType - hazelnuts;
            totalAvailable = MainStorage.maxStorage - Chocolates.Count();
        }
    }
}