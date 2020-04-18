﻿using ChocolateTycoon.Data;
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

        public FactoryRepository Factories { get; private set; }

        public MainStorageRepository MainStorages { get; private set; }

        public ProductionUnitRepository ProductionUnits { get; private set; }

        public StorageUnitRepository StorageUnits { get; private set; }

        public ChocolateRepository Chocolates { get; set; }

        public SafeRepository Safes { get; set; }

        public EmployeeRepository Employees { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Stores = new StoreRepository(db);
            Factories = new FactoryRepository(db);
            MainStorages = new MainStorageRepository(db);
            ProductionUnits = new ProductionUnitRepository(db);
            StorageUnits = new StorageUnitRepository(db);
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