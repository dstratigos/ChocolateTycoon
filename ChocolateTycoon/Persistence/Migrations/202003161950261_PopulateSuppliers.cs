namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateSuppliers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('WholeSale Inc.', 8000, 0.6, 800)");
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('SupplyMart Ltd', 9000, 0.55, 750)");
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('Supplies-R-Us Co.', 10000, 0.5, 500)");
        }

        public override void Down()
        {
            Sql("DELETE FROM Suppliers WHERE Id IN(1, 2, 3)");
        }
    }
}
