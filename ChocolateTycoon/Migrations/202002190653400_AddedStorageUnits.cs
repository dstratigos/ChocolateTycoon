namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStorageUnits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StorageUnits",
                c => new
                    {
                        FactoryID = c.Int(nullable: false),
                        RawMaterialAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FactoryID)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .Index(t => t.FactoryID);
            
            AddColumn("dbo.Chocolates", "StorageUnit_FactoryID", c => c.Int());
            CreateIndex("dbo.Chocolates", "StorageUnit_FactoryID");
            AddForeignKey("dbo.Chocolates", "StorageUnit_FactoryID", "dbo.StorageUnits", "FactoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorageUnits", "FactoryID", "dbo.Factories");
            DropForeignKey("dbo.Chocolates", "StorageUnit_FactoryID", "dbo.StorageUnits");
            DropIndex("dbo.StorageUnits", new[] { "FactoryID" });
            DropIndex("dbo.Chocolates", new[] { "StorageUnit_FactoryID" });
            DropColumn("dbo.Chocolates", "StorageUnit_FactoryID");
            DropTable("dbo.StorageUnits");
        }
    }
}
