namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductionUnit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductionUnits",
                c => new
                    {
                        FactoryID = c.Int(nullable: false),
                        MaxProductionPerDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactoryID)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .Index(t => t.FactoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionUnits", "FactoryID", "dbo.Factories");
            DropIndex("dbo.ProductionUnits", new[] { "FactoryID" });
            DropTable("dbo.ProductionUnits");
        }
    }
}
