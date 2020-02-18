namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FactoryAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Factories", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Factories", "Name", c => c.String());
        }
    }
}
