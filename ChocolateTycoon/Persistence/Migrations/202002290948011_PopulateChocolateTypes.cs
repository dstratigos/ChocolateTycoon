namespace ChocolateTycoon.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateChocolateTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ChocolateStatus (Name) VALUES ('Factory')");
            Sql("INSERT INTO ChocolateStatus (Name) VALUES ('Main Storage')");
            Sql("INSERT INTO ChocolateStatus (Name) VALUES ('Store')");
            Sql("INSERT INTO ChocolateStatus (Name) VALUES ('Sold')");
            Sql("INSERT INTO ChocolateStatus (Name) VALUES ('Charity')");
        }
        
        public override void Down()
        {
        }
    }
}
