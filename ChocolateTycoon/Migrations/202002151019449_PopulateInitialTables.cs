namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateInitialTables : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Stores (Name, Level) VALUES ('Ppg First', 1)");
            Sql("INSERT INTO Stores (Name, Level) VALUES ('Ppg Second', 1)");
            Sql("INSERT INTO Factories (Name, Level) VALUES ('Athens', 1)");
            Sql("INSERT INTO Factories (Name, Level) VALUES ('Kozani', 1)");
            Sql("INSERT INTO Safes (Deposit) VALUES (10000)");
            Sql("INSERT INTO Players (Username, Password) VALUES ('OlaKala', 'EnaPassword')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Stores WHERE ID IN (1, 2)");
            Sql("DELETE FROM Factories WHERE ID IN (1, 2)");
            Sql("DELETE FROM Safes");
            Sql("DELETE FROM Players WHERE ID = 1");
        }
    }
}
