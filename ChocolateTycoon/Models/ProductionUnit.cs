using ChocolateTycoon.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class ProductionUnit
    {
        [Key]
        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public int MaxProductionPerDay { get; set; }

        public Factory Factory { get; set; }

        public ProductionUnit()
        {
            MaxProductionPerDay = 200;
        }

        // Total amount of Raw Materials needed per production cycle
        // calculated by multiplying percentage of each type related
        // to dailyProduction by materialCost per type
        public double MaterialsNeeded()
        {
            var dark = MaxProductionPerDay * 0.45 * MaterialsCost.Dark;
            var white = MaxProductionPerDay * 0.05 * MaterialsCost.White;
            var milk = MaxProductionPerDay * 0.3 * MaterialsCost.Milk;
            var almMilk = MaxProductionPerDay * 0.1 * MaterialsCost.AlmondMilk;
            var hzlMilk = MaxProductionPerDay * 0.1 * MaterialsCost.HazelnutMilk;

            return dark + white + milk + almMilk + hzlMilk;
        }

        // Creates the daily amount of products
        public List<Chocolate> DailyProduction()
        {
            var materialsNeeded = MaterialsNeeded();
            var products = new List<Chocolate>();

            for (var i = 0; i < MaxProductionPerDay * 0.45; i++)
            {
                var chocolate = new Chocolate
                {
                    ChocolateType = ChocolateType.Dark,
                    ChocolateStatusId = 1
                };

                products.Add(chocolate);
            }
            for (var i = 0; i < MaxProductionPerDay * 0.05; i++)
            {
                var chocolate = new Chocolate
                {
                    ChocolateType = ChocolateType.White,
                    ChocolateStatusId = 1
                };

                products.Add(chocolate);
            }
            for (var i = 0; i < MaxProductionPerDay * 0.3; i++)
            {
                var chocolate = new Chocolate
                {
                    ChocolateType = ChocolateType.Milk,
                    ChocolateStatusId = 1
                };

                products.Add(chocolate);
            }
            for (var i = 0; i < MaxProductionPerDay * 0.1; i++)
            {
                var chocolate = new Chocolate
                {
                    ChocolateType = ChocolateType.AlmondMilk,
                    ChocolateStatusId = 1
                };

                products.Add(chocolate);
            }
            for (var i = 0; i < MaxProductionPerDay * 0.1; i++)
            {
                var chocolate = new Chocolate
                {
                    ChocolateType = ChocolateType.HazelnutMilk,
                    ChocolateStatusId = 1
                };

                products.Add(chocolate);
            }

            Factory.StorageUnit.RawMaterialAmount -= materialsNeeded;

            return products;
        }
    }
}