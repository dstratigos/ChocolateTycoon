namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainStorageName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainStorages", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MainStorages", "Name");
        }
    }
}
