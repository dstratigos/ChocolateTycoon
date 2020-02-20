namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainStorageInChocolates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chocolates", "MainStorageID", "dbo.MainStorages");
            DropIndex("dbo.Chocolates", new[] { "MainStorageID" });
            RenameColumn(table: "dbo.Chocolates", name: "MainStorageID", newName: "MainStorage_ID");
            AlterColumn("dbo.Chocolates", "MainStorage_ID", c => c.Int());
            CreateIndex("dbo.Chocolates", "MainStorage_ID");
            AddForeignKey("dbo.Chocolates", "MainStorage_ID", "dbo.MainStorages", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chocolates", "MainStorage_ID", "dbo.MainStorages");
            DropIndex("dbo.Chocolates", new[] { "MainStorage_ID" });
            AlterColumn("dbo.Chocolates", "MainStorage_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Chocolates", name: "MainStorage_ID", newName: "MainStorageID");
            CreateIndex("dbo.Chocolates", "MainStorageID");
            AddForeignKey("dbo.Chocolates", "MainStorageID", "dbo.MainStorages", "ID", cascadeDelete: true);
        }
    }
}
