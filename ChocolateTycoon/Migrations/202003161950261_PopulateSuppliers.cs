namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateSuppliers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('Antonis', 8000, 0.6, 0)");
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('Dimitris', 10000, 0.5, 0)");
            Sql("INSERT INTO Suppliers (Name, OfferAmount, PricePerKilo, ShippedAmount) VALUES ('Eleni', 9000, 0.55, 0)");
        }

        public override void Down()
        {
            Sql("DELETE FROM Suppliers WHERE Id IN(1, 2, 3)");
        }
    }
}
