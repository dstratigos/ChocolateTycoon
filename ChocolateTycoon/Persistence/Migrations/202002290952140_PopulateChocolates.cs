namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateChocolates : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Chocolates (ChocolateType, Price, ChocolateStatusId) VALUES (1, 5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, ChocolateStatusId) VALUES (2, 3.5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, ChocolateStatusId) VALUES (3, 2.7, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, ChocolateStatusId) VALUES (4, 4.8, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, ChocolateStatusId) VALUES (5, 5.5, 1)");
            Sql("INSERT INTO Chocolates (ChocolateType, Price, ChocolateStatusId) VALUES (3, 2.7, 1)");

        }

        public override void Down()
        {
            Sql("DELETE FROM Chocolates WHERE ID IN (1, 2, 3, 4, 5, 6)");
        }
    }
}
