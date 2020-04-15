﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Turn
    {
        public static string TurnMessage { get; set; }
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

            var wages = Safe.calculateTotalWages(Employees);
            if (Safe.Deposit >= wages)
            {
                Safe.Deposit -= wages;

                foreach (var productionUnit in ProductionUnits)
                    productionUnit.ProducedDailyProduction = false;

                TurnMessage = $"{wages} have been deducted from vault. Go make some money!";

                return;
            }

            TurnMessage = $"Not enough money to pay wages! This is going to be a loooong day!";
        }
    }
}