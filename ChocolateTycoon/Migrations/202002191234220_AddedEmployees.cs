namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreID = c.Int(),
                        FactoryID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.FactoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.Employees", "FactoryID", "dbo.Factories");
            DropIndex("dbo.Employees", new[] { "FactoryID" });
            DropIndex("dbo.Employees", new[] { "StoreID" });
            DropTable("dbo.Employees");
        }
    }
}
