namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreForeignKeysMainStorageSafe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "MainStorageID", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.Stores", "SafeID", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Stores", "MainStorageID");
            CreateIndex("dbo.Stores", "SafeID");
            AddForeignKey("dbo.Stores", "MainStorageID", "dbo.MainStorages", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Stores", "SafeID", "dbo.Safes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "SafeID", "dbo.Safes");
            DropForeignKey("dbo.Stores", "MainStorageID", "dbo.MainStorages");
            DropIndex("dbo.Stores", new[] { "SafeID" });
            DropIndex("dbo.Stores", new[] { "MainStorageID" });
            DropColumn("dbo.Stores", "SafeID");
            DropColumn("dbo.Stores", "MainStorageID");
        }
    }
}
