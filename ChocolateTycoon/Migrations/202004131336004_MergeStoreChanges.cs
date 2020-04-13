namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeStoreChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chocolates", "ChocolateStatusId", "dbo.ChocolateStatus");
            RenameColumn(table: "dbo.Stores", name: "Safe_ID", newName: "SafeID");
            RenameIndex(table: "dbo.Stores", name: "IX_Safe_ID", newName: "IX_SafeID");
            AddColumn("dbo.Stores", "MainStorageID", c => c.Int());
            AddColumn("dbo.Stores", "ImageBase64", c => c.String());
            AlterColumn("dbo.Stores", "Name", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Stores", "MainStorageID");
            AddForeignKey("dbo.Stores", "MainStorageID", "dbo.MainStorages", "ID");
            AddForeignKey("dbo.Chocolates", "ChocolateStatusId", "dbo.ChocolateStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chocolates", "ChocolateStatusId", "dbo.ChocolateStatus");
            DropForeignKey("dbo.Stores", "MainStorageID", "dbo.MainStorages");
            DropIndex("dbo.Stores", new[] { "MainStorageID" });
            AlterColumn("dbo.Stores", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Stores", "ImageBase64");
            DropColumn("dbo.Stores", "MainStorageID");
            RenameIndex(table: "dbo.Stores", name: "IX_SafeID", newName: "IX_Safe_ID");
            RenameColumn(table: "dbo.Stores", name: "SafeID", newName: "Safe_ID");
            AddForeignKey("dbo.Chocolates", "ChocolateStatusId", "dbo.ChocolateStatus", "Id", cascadeDelete: true);
        }
    }
}
