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

        public IEnumerable<Chocolate> GetMainStorageChocolatesDark()
        {
            return _db.Chocolates
                .Where(c => c.ChocolateStatusId == 2 && c.ChocolateType == ChocolateType.Dark).Take(68);
        }

        public IEnumerable<Chocolate> GetMainStorageChocolatesAlmond()
        {
            return _db.Chocolates
                .Where(c => c.ChocolateStatusId == 2 && c.ChocolateType == ChocolateType.AlmondMilk).Take(15);
        }

        public IEnumerable<Chocolate> GetMainStorageChocolatesHazelnut()
        {
            return _db.Chocolates
                .Where(c => c.ChocolateStatusId == 2 && c.ChocolateType == ChocolateType.HazelnutMilk).Take(15);
        }

        public IEnumerable<Chocolate> GetMainStorageChocolatesMilk()
        {
            return _db.Chocolates
                .Where(c => c.ChocolateStatusId == 2 && c.ChocolateType == ChocolateType.Milk).Take(45);
        }

        public IEnumerable<Chocolate> GetMainStorageChocolatesWhite()
        {
            return _db.Chocolates
                .Where(c => c.ChocolateStatusId == 2 && c.ChocolateType == ChocolateType.Dark).Take(7);
        }

        public void Add(List<Chocolate> chocolates)
        {
            _db.Chocolates.AddRange(chocolates);
        }
    }
}