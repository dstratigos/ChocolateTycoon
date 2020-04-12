namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProducedDailyProduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnits", "ProducedDailyProduction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnits", "ProducedDailyProduction");
        }
    }
}
