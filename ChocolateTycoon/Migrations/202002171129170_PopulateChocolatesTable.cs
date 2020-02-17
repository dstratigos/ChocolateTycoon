namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateChocolatesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Chocolates (ChocolateType, Price, MainStorageID) VALUES (1, 5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, MainStorageID) VALUES (1, 5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, MainStorageID) VALUES (2, 3.5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, MainStorageID) VALUES (3, 4.5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, MainStorageID) VALUES (4, 4.5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, MainStorageID) VALUES (5, 4, 1)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Chocolates WHERE ID IN (1, 2, 3, 4, 5, 6)");
        }
    }
}
