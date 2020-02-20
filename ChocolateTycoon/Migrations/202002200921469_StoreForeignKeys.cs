namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stores", "MainStorageID", "dbo.MainStorages");
            DropForeignKey("dbo.Stores", "SafeID", "dbo.Safes");
            DropIndex("dbo.Stores", new[] { "MainStorageID" });
            DropIndex("dbo.Stores", new[] { "SafeID" });
            RenameColumn(table: "dbo.Stores", name: "MainStorageID", newName: "MainStorage_ID");
            RenameColumn(table: "dbo.Stores", name: "SafeID", newName: "Safe_ID");
            AlterColumn("dbo.Stores", "MainStorage_ID", c => c.Int());
            AlterColumn("dbo.Stores", "Safe_ID", c => c.Int());
            CreateIndex("dbo.Stores", "MainStorage_ID");
            CreateIndex("dbo.Stores", "Safe_ID");
            AddForeignKey("dbo.Stores", "MainStorage_ID", "dbo.MainStorages", "ID");
            AddForeignKey("dbo.Stores", "Safe_ID", "dbo.Safes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "Safe_ID", "dbo.Safes");
            DropForeignKey("dbo.Stores", "MainStorage_ID", "dbo.MainStorages");
            DropIndex("dbo.Stores", new[] { "Safe_ID" });
            DropIndex("dbo.Stores", new[] { "MainStorage_ID" });
            AlterColumn("dbo.Stores", "Safe_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Stores", "MainStorage_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Stores", name: "Safe_ID", newName: "SafeID");
            RenameColumn(table: "dbo.Stores", name: "MainStorage_ID", newName: "MainStorageID");
            CreateIndex("dbo.Stores", "SafeID");
            CreateIndex("dbo.Stores", "MainStorageID");
            AddForeignKey("dbo.Stores", "SafeID", "dbo.Safes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Stores", "MainStorageID", "dbo.MainStorages", "ID", cascadeDelete: true);
        }
    }
}
