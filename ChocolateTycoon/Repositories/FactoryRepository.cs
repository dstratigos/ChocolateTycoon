using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChocolateTycoon.Repositories
{
    public class FactoryRepository
    {
        private readonly ApplicationDbContext _db;

        public FactoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Factory> GetFactories()
        {
            return _db.Factories;
        }

        public IEnumerable<Factory> GetFactoriesWithProductionAndStorageUnit()
        {
            return _db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit);
        }

        public IEnumerable<Factory> GetFactoriesWithStorageUnitAndSupplier()
        {
            return _db.Factories
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier);
        }

        public Factory GetFactoryAllInclusive(int id)
        {
            return _db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);
        }

        public Factory GetFactoryMinusSupplier(int id)
        {
            return _db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
                .SingleOrDefault(f => f.ID == id);
        }

        public Factory GetFactory(int id)
        {
            return _db.Factories
                .SingleOrDefault(f => f.ID == id);
        }

        public void Add(Factory factory)
        {
            _db.Factories.Add(factory);
        }

        public void Remove(Factory factory)
        {
            _db.Factories.Remove(factory);
        }
    }
}