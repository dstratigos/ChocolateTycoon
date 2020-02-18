using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Factory
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Please enter a name up to 100 characters")]
        public string Name { get; set; }
        public byte Level { get; set; }

        public ProductionUnit ProductionUnit { get; set; }

    }
}