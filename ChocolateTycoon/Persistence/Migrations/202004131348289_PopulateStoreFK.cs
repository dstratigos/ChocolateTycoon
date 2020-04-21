namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStoreFK : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Stores SET SafeID = 1, MainStorageID = 1 WHERE ID = 1");
            Sql("UPDATE Stores SET SafeID = 1, MainStorageID = 1 WHERE ID = 2");

            DropIndex("dbo.Stores", new[] { "MainStorageID" });
            DropIndex("dbo.Stores", new[] { "SafeID" });
            AlterColumn("dbo.Stores", "MainStorageID", c => c.Int(nullable: false));
            AlterColumn("dbo.Stores", "SafeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Stores", "MainStorageID");
            CreateIndex("dbo.Stores", "SafeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stores", new[] { "SafeID" });
            DropIndex("dbo.Stores", new[] { "MainStorageID" });
            AlterColumn("dbo.Stores", "SafeID", c => c.Int());
            AlterColumn("dbo.Stores", "MainStorageID", c => c.Int());
            CreateIndex("dbo.Stores", "SafeID");
            CreateIndex("dbo.Stores", "MainStorageID");
        }
    }
}
