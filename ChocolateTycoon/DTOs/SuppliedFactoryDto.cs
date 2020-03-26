using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.DTOs
{
    public class SuppliedFactoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int supplierId { get; set; }

    }
}