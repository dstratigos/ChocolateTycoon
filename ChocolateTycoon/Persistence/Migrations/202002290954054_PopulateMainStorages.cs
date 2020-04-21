namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMainStorages : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MainStorages (Name) VALUES ('Main Storage')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM MainStorages WHERE Id = 1");
        }
    }
}
