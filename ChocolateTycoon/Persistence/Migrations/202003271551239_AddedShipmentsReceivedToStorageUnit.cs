namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShipmentsReceivedToStorageUnit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StorageUnits", "ShipmentsReceived", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StorageUnits", "ShipmentsReceived");
        }
    }
}
