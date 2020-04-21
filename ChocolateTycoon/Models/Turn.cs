using System.Collections.Generic;
using System.Linq;

namespace ChocolateTycoon.Models
{
    public class Turn
    {
        public static string TurnMessage { get; set; }
        public List<Store> Stores { get; private set; }
        public List<ProductionUnit> ProductionUnits { get; private set; }
        public List<Employee> Employees { get; private set; }
        public Safe Safe { get; private set; }

        protected Turn()
        { }

        public Turn(List<Store> stores, List<ProductionUnit> productionUnits, List<Employee> employees, Safe safe)
        {
            Stores = stores;
            ProductionUnits = productionUnits;
            Employees = employees;
            Safe = safe;
        }

        private bool LooseEnds()
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
            TurnMessage = "";

            var wages = Safe.CalculateTotalWages(Employees);
            if (Safe.Deposit >= wages)
            {
                Safe.Deposit -= wages;

                foreach (var productionUnit in ProductionUnits)
                    productionUnit.ProducedDailyProduction = false;

                foreach (var store in Stores)
                    store.CompletedDailySales = false;

                TurnMessage = $"{wages} have been deducted from vault. Go make some money!";

                return;
            }

            TurnMessage = $"Not enough money to pay wages! This is going to be a loooong day!";
        }
    }
}