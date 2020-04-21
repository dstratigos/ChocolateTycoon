namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStorageUnits : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StorageUnits (FactoryID, RawMaterialAmount) VALUES (1, 2000)");
            Sql("INSERT INTO StorageUnits (FactoryID, RawMaterialAmount) VALUES (2, 50)");
        }

        public override void Down()
        {
            Sql("DELETE FROM StorageUnits WHERE FactoryID IN (1, 2)");
        }
    }
}
