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
    }
}