using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public enum ChocolateType
    {
        [Display(Name = "Dark")]
        Dark = 1,

        [Display(Name = "Milk")]
        Milk,

        [Display(Name = "Milk w Almonds")]
        AlmondMilk,

        [Display(Name = "Milk w Hazelnuts")]
        HazelnutMilk,

        [Display(Name = "White")]
        White
    }


    public class Chocolate
    {
        public int ID { get; set; }

        public ChocolateType ChocolateType { get; set; }

        public decimal Price { get; set; }

        public int ChocolateStatusId { get; set; }
        public ChocolateStatus Status { get; set; }

        public int? StoreId { get; set; }
        public Store Store { get; set; }

        public void MarkAsSold()
        {
            ChocolateStatusId = 4;
        }

        public void ToStore()
        {
            ChocolateStatusId = 3;
        }
    }
}