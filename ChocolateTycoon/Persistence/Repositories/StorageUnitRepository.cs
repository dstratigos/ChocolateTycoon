using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class StorageUnitRepository : IStorageUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public StorageUnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(StorageUnit storageUnit)
        {
            _db.StorageUnits.Add(storageUnit);
        }
    }
}