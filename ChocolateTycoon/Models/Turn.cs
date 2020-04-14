using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Turn
    {
        public string TurnMessage { get; private set; }
        public List<ProductionUnit> ProductionUnits { get; private set; }
        public List<Employee> Employees { get; private set; }
        public Safe Safe { get; private set; }

        protected Turn()
        { }

        public Turn(List<ProductionUnit> productionUnits, List<Employee> employees, Safe safe)
        {
            ProductionUnits = productionUnits;
            Employees = employees;
            Safe = safe;
        }

        public bool LooseEnds()
        {
            var notProduced = ProductionUnits.Any(pu => pu.ProducedDailyProduction == false);

            if (notProduced)
            {
                TurnMessage = "Some Factories have not reached their daily production capacity!";
                return true;
            };

            return false;
        }

        public void EndTurn()
        {
            foreach (var productionUnit in ProductionUnits)
                productionUnit.ProducedDailyProduction = false;

            var wages = Safe.calculateTotalWages(Employees);
            Safe.Deposit -= wages;
        }
    }
}