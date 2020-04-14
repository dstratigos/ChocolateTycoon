using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChocolateTycoon.Repositories
{
    public class StoreRepository
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

        public Store GetStoreWithAllDetails(int id)
        {
            return _db.Stores
                .Include(s => s.Employees)
                .Include(s => s.Chocolates)
                .Include(s => s.MainStorage)
                .Include(s => s.Safe)
                .Where(s => s.ID == id).SingleOrDefault();
        }

        public IEnumerable<Store> GetStoresWithSafe()
        {
            return _db.Stores
                .Include(s => s.Safe);
        }

        public Store GetStoreWithChocolates(int id)
        {
            return _db.Stores
                .Include(s => s.Chocolates)
                .SingleOrDefault(s => s.ID == id);
        }
    }
}