namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stock : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Stores", "Stock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "Stock", c => c.Int(nullable: false));
        }
    }
}
