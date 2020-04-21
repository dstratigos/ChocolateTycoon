namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chocolates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ChocolateType = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChocolateStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChocolateStatus", t => t.ChocolateStatusId, cascadeDelete: true)
                .Index(t => t.ChocolateStatusId);
            
            CreateTable(
                "dbo.ChocolateStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreID = c.Int(),
                        FactoryID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.FactoryID);
            
            CreateTable(
                "dbo.Factories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductionUnits",
                c => new
                    {
                        FactoryID = c.Int(nullable: false),
                        MaxProductionPerDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactoryID)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .Index(t => t.FactoryID);
            
            CreateTable(
                "dbo.StorageUnits",
                c => new
                    {
                        FactoryID = c.Int(nullable: false),
                        RawMaterialAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FactoryID)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .Index(t => t.FactoryID);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Level = c.Byte(nullable: false),
                        Safe_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Safes", t => t.Safe_ID)
                .Index(t => t.Safe_ID);
            
            CreateTable(
                "dbo.Safes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Deposit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MainStorages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Stores", "Safe_ID", "dbo.Safes");
            DropForeignKey("dbo.Employees", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.StorageUnits", "FactoryID", "dbo.Factories");
            DropForeignKey("dbo.ProductionUnits", "FactoryID", "dbo.Factories");
            DropForeignKey("dbo.Employees", "FactoryID", "dbo.Factories");
            DropForeignKey("dbo.Chocolates", "ChocolateStatusId", "dbo.ChocolateStatus");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Stores", new[] { "Safe_ID" });
            DropIndex("dbo.StorageUnits", new[] { "FactoryID" });
            DropIndex("dbo.ProductionUnits", new[] { "FactoryID" });
            DropIndex("dbo.Employees", new[] { "FactoryID" });
            DropIndex("dbo.Employees", new[] { "StoreID" });
            DropIndex("dbo.Chocolates", new[] { "ChocolateStatusId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Players");
            DropTable("dbo.MainStorages");
            DropTable("dbo.Safes");
            DropTable("dbo.Stores");
            DropTable("dbo.StorageUnits");
            DropTable("dbo.ProductionUnits");
            DropTable("dbo.Factories");
            DropTable("dbo.Employees");
            DropTable("dbo.ChocolateStatus");
            DropTable("dbo.Chocolates");
        }
    }
}
