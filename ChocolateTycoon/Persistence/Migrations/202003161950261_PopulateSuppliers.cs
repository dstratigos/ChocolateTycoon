namespace ChocolateTycoon.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateSuppliers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('WholeSale Inc.', 800, 0.6, 80)");
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('SupplyMart Ltd', 900, 0.55, 75)");
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('Supplies-R-Us Co.', 1000, 0.5, 50)");
        }

        public override void Down()
        {
            Sql("DELETE FROM Suppliers WHERE Id IN(1, 2, 3)");
        }
    }
}
