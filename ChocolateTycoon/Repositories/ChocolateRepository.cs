using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChocolateTycoon.Repositories
{
    public class ChocolateRepository
    {
        private readonly ApplicationDbContext _db;

        public ChocolateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Chocolate> GetChocolatesWithStatus(int storeId)
        {
            return _db.Chocolates
                .Include(c => c.Status)
                .Where(c => c.StoreId == storeId);
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