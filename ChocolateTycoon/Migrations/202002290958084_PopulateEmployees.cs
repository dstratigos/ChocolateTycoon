namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateEmployees : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, StoreID) VALUES ('Doe', 'John', 0, 1500, 1)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, StoreID) VALUES ('Doe', 'Jane', 1, 900, 1)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, FactoryID) VALUES ('Jack', 'Black', 2, 800, 1)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, FactoryID) VALUES ('Fox', 'Samantha', 0, 1700, 1)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary) VALUES ('Middleton', 'Pipa', 0, 2500)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, FactoryID) VALUES ('Smith', 'Robert', 2, 800, 1)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, StoreID) VALUES ('Professor', 'The', 0, 2200, 2)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary, FactoryID) VALUES ('Dean', 'James', 0, 1800, 2)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary) VALUES ('Knightley', 'Keira', 1, 1000)");
            Sql("INSERT INTO Employees (FirstName, LastName, Position, Salary) VALUES ('Ashimi', 'Maria', 2, 700)");
        }

        public override void Down()
        {
            Sql("DELETE FROM Employees WHERE ID IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)");
        }
    }
}
