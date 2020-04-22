using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class ChocolateRepository : IChocolateRepository
    {
        private readonly ApplicationDbContext _db;

        public ChocolateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Chocolate> GetStoreChocolates(int storeId)
        {
            return _db.Chocolates
                .Where(c => c.StoreId == storeId && c.ChocolateStatusId == 3);
        }

        public IEnumerable<Chocolate> GetMainStorageChocolates()
        {
            return _db.Chocolates
                .Where(c => c.ChocolateStatusId == 2);
        }

        public void Add(List<Chocolate> chocolates)
        {
            _db.Chocolates.AddRange(chocolates);
        }
    }
}