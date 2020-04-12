using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Turn
    {
        public static string TurnMessage { get; private set; }
        public static bool LooseEnds(List<Factory> factories)
        {
            var notProduced = factories.Any(f => f.ProductionUnit.ProducedDailyProduction == false);

            if (notProduced)
            {
                TurnMessage = $"";
                return true;
            }
                

            return false;
        }
    }
}