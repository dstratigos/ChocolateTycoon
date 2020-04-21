namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProductionUnits : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ProductionUnits (FactoryID, MaxProductionPerDay) VALUES (1, 200)");
            Sql("INSERT INTO ProductionUnits (FactoryID, MaxProductionPerDay) VALUES (2, 200)");
        }

        public override void Down()
        {
            Sql("DELETE FROM ProductionUnits WHERE FactoryID IN (1, 2)");
        }
    }
}
