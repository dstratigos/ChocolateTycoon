namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeysChocolate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chocolates", "MainStorageID", c => c.Int(nullable: false));
            AddColumn("dbo.Chocolates", "Store_ID", c => c.Int());
            CreateIndex("dbo.Chocolates", "MainStorageID");
            CreateIndex("dbo.Chocolates", "Store_ID");
            AddForeignKey("dbo.Chocolates", "MainStorageID", "dbo.MainStorages", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Chocolates", "Store_ID", "dbo.Stores", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chocolates", "Store_ID", "dbo.Stores");
            DropForeignKey("dbo.Chocolates", "MainStorageID", "dbo.MainStorages");
            DropIndex("dbo.Chocolates", new[] { "Store_ID" });
            DropIndex("dbo.Chocolates", new[] { "MainStorageID" });
            DropColumn("dbo.Chocolates", "Store_ID");
            DropColumn("dbo.Chocolates", "MainStorageID");
        }
    }
}
