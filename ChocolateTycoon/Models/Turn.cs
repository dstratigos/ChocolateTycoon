using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Turn
    {
        public static string TurnMessage { get; private set; }
        public static bool LooseEnds(List<ProductionUnit> productionUnits)
        {
            var notProduced = productionUnits.Any(pu => pu.ProducedDailyProduction == false);

            if (notProduced)
            {
                TurnMessage = "Some Factories have not reached their daily production capacity!";
                return true;
            };

            return false;
        }

        public static void EndTurn(List<ProductionUnit> productionUnits)
        {
            foreach (var productionUnit in productionUnits)
                productionUnit.ProducedDailyProduction = false;
        }
    }
}