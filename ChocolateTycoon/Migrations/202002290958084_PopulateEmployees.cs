namespace ChocolateTycoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateEmployees : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, StoreID) VALUES ('Doe', 'John', 1, 60, 1)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, StoreID) VALUES ('Doe', 'Jane', 2, 36, 1)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, FactoryID) VALUES ('Jack', 'Black', 3, 40, 1)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, FactoryID) VALUES ('Fox', 'Samantha', 0, 68, 1)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary) VALUES ('Middleton', 'Pipa', 0, 1700)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, FactoryID) VALUES ('Smith', 'Robert', 3, 40, 1)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, StoreID) VALUES ('Professor', 'The', 1, 60, 2)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary, FactoryID) VALUES ('Dean', 'James', 0, 68, 2)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary) VALUES ('Knightley', 'Keira', 2, 36)");
            Sql("INSERT INTO Employees (LastName, FirstName, Position, Salary) VALUES ('Ashimi', 'Maria', 3, 40)");
        }

        public override void Down()
        {
            Sql("DELETE FROM Employees WHERE ID IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)");
        }
    }
}
