namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChocolateStoreFk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chocolates", "StoreId", c => c.Int());
            CreateIndex("dbo.Chocolates", "StoreId");
            AddForeignKey("dbo.Chocolates", "StoreId", "dbo.Stores", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chocolates", "StoreId", "dbo.Stores");
            DropIndex("dbo.Chocolates", new[] { "StoreId" });
            DropColumn("dbo.Chocolates", "StoreId");
        }
    }
}
