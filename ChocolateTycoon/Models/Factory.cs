using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Factory
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Please enter at least 3 characters")]
        public string Name { get; set; }
        public byte Level { get; private set; }
        private const int _createCost = 2000;
        public static int CreateCost => _createCost;

        public int? SupplierId { get; set; }

        public ProductionUnit ProductionUnit { get; set; }
        public StorageUnit StorageUnit { get; set; }
        public List<Employee> Employees { get; set; }
        public Supplier Supplier { get; set; }


        public Factory()
        {
            Level = 1;
            Employees = new List<Employee>();
        }

        public bool NameExists(IEnumerable<Factory> factories)
        {
            foreach (var factory in factories)
                if (Name == factory.Name)
                    return true;

            return false;
        }

        // starts the production sequence and sets the appropraite messages
        public void Produce(MainStorage mainStorage, List<Chocolate> chocolatesStored)
        {
            var materialsNeeded = ProductionUnit.MaterialsNeeded();
            var materialsSuffice = StorageUnit.MaterialsSuffice(materialsNeeded);

            if (!ProductionUnit.ProducedDailyProduction)
            {
                if (PersonelSuffice() && materialsSuffice)
                {
                    var products = ProductionUnit.DailyProduction();

                    mainStorage.newProducts.AddRange(products);

                    mainStorage.SortProducts(chocolatesStored);

                    ProductionUnit.ProducedDailyProduction = true;
                }
                else if (!PersonelSuffice())
                    Message.SetErrorMessage(MessageEnum.PersonelError);
                else
                    Message.SetErrorMessage(MessageEnum.RawMaterialsError);
            }
            else
            {
                Message.SetErrorMessage(MessageEnum.ProductionTurnError);
                Message.SetMainStorageInfo(0 ,0);
            }
                
        }

        // checks if the factory personel meets the required minimum for the factory to operate
        public bool PersonelSuffice()
        {
            var managersEmployed = Employees.Where(e => e.Position == EmployeePosition.FactoryManager).Count();
            var employeesEmployed = Employees.Where(e => e.Position == EmployeePosition.ProductionEngineer).Count();

            if (Level == 1)
            {
                if (managersEmployed >= 1 && employeesEmployed >= 2)
                    return true;
            }
            else if (Level == 2)
            {
                if (managersEmployed >= 2 && employeesEmployed >= 4)
                    return true;
            }

            return false;
        }

        // breaks the Contract with an active Supplier and returns a message
        public string BreakContract(Safe vault)
        {
            if (Supplier != null)
            {
                decimal shipments = Convert.ToDecimal(StorageUnit.ShipmentsReceived);
                decimal offerAmount = Convert.ToDecimal(Supplier.OfferAmount);
                var penalty = (offerAmount - shipments) * Supplier.PricePerKilo * 0.5M;
                var supplierName = Supplier.Name;

                if (vault.MoneySuffice(penalty))
                {
                    Supplier = null;
                    StorageUnit.ResetSupplier();
                    vault.WithdrawAmount(penalty);

                    return $"{Name} Factory broke it's Contract with {supplierName}. You got charged with {penalty:F2}";
                }

                return $"The penalty is {penalty:F2}! Seems you're stuck with these guys for now...";
            }

            return "Something went wrong. Try again";
        }

        // Makes a new contract with the Supplier it takes as a parameter
        // and returns a message on success
        public string MakeContract(Supplier supplier)
        {
            Supplier = supplier;

            return $"A Contract has been made between {supplier.Name} and {Name} Factory.";
        }

        // Checks whether a Factory from a list of Factories, has an active Contract with
        // any Supplier and returns a boolean value
        public static bool HasActiveContract(List<Factory> factories, int factoryId)
        {
            var suppliedFactories = factories.Where(f => f.Supplier != null).ToList();
            var hasSupplier = suppliedFactories.Exists(f => f.ID == factoryId);

            if (suppliedFactories != null)
                if (hasSupplier)
                    return true;

            return false;
        }

        public void DoDelete(Safe vault)
        {
            var totalCost = _createCost;

            if (ProductionUnit != null)
                totalCost += ProductionUnit.CreateCost;

            if (StorageUnit != null)
                totalCost += StorageUnit.CreateCost;

            ProductionUnit = null;
            StorageUnit = null;
            Employees.Clear();

            vault.Refund(totalCost);
        }
    }
}
