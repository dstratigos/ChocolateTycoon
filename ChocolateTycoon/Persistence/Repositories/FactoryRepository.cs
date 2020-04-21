using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChocolateTycoon.Repositories
{
    public class FactoryRepository : IFactoryRepository
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

        public IEnumerable<Factory> GetFactoriesWithSupplier()
        {
            return _db.Factories.Include(f => f.Supplier);
        }

        public Factory GetFactory(int id)
        {
            return _db.Factories
                .SingleOrDefault(f => f.ID == id);
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

        public Factory GetFactoryWithStorageUnitAndSupplier(int id)
        {
            return _db.Factories
                 .Include(f => f.StorageUnit)
                 .Include(f => f.Supplier)
                 .Single(f => f.ID == id);
                }

        public Factory GetFactoryMinusSupplier(int id)
        {
            return _db.Factories
                .Include(f => f.ProductionUnit)
                .Include(f => f.StorageUnit)
                .Include(f => f.Employees)
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