using ChocolateTycoon.Models;
using ChocolateTycoon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Services
{
    public class FactoryService
    {
        public int PopulateChocolates(List<Chocolate> chocolates)
        {
            var count = chocolates.Where(c => c.ChocolateStatusId == 1).Count();

            return count;
        }
    }
}