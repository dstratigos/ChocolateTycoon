namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailySalesProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "CompletedDailySales", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "CompletedDailySales");
        }
    }
}
