namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Stock", c => c.Int(nullable: false));
            DropColumn("dbo.Stores", "ImageBase64");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "ImageBase64", c => c.String());
            DropColumn("dbo.Stores", "Stock");
        }
    }
}
