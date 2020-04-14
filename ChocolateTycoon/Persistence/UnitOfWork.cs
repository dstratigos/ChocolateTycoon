using ChocolateTycoon.Data;
using ChocolateTycoon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository Stores { get; private set; }

        public ChocolateRepository Chocolates { get; set; }

        public SafeRepository Safes { get; set; }

        public EmployeeRepository Employees { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Stores = new StoreRepository(db);
            Chocolates = new ChocolateRepository(db);
            Safes = new SafeRepository(db);
            Employees = new EmployeeRepository(db);
        }

        public void Complete()
        {
            _db.SaveChanges();
        }
    }
}