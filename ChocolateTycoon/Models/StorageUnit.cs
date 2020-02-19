using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class StorageUnit
    {
        [Key]
        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public double RawMaterialAmount { get; set; }       



        public Factory Factory { get; set; }
        public ICollection<Chocolate> Chocolates { get; set; }
    }
}