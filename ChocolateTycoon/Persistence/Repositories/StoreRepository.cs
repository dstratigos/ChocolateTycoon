using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Store GetStore(int id)
        {
            return _db.Stores
                .Single(s => s.ID == id);
        }

        public IEnumerable<Store> GetStores()
        {
            return _db.Stores;
        }

        public IEnumerable<Store> GetStoresWithAllDetails()
        {
            return _db.Stores
                .Include(s => s.Employees)
                .Include(s => s.Chocolates)
                .Include(s => s.MainStorage)
                .Include(s => s.Safe);
        }

        public Store GetStoreWithAllDetails(int id)
        {
            return _db.Stores
                .Include(s => s.Employees)
                .Include(s => s.Chocolates)
                .Include(s => s.MainStorage)
                .Include(s => s.Safe)
                .Where(s => s.ID == id).SingleOrDefault();
        }

        public Store GetStoreWithSafe(int id)
        {
            return _db.Stores
                .Include(s => s.Safe)
                .Where(s => s.ID == id)
                .SingleOrDefault();
        }

        //public Store GetStoreWithChocolates(int id)
        //{
        //    return _db.Stores
        //        .Include(s => s.Chocolates)
        //        .SingleOrDefault(s => s.ID == id);
        //}

        public void Add(Store store)
        {
            _db.Stores.Add(store);
        }

        public void Remove(Store store)
        {
            _db.Stores.Remove(store);
        }
    }
}