namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFactorySupplierRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Factories", "SupplierId", c => c.Int());
            CreateIndex("dbo.Factories", "SupplierId");
            AddForeignKey("dbo.Factories", "SupplierId", "dbo.Suppliers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Factories", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Factories", new[] { "SupplierId" });
            DropColumn("dbo.Factories", "SupplierId");
        }
    }
}
