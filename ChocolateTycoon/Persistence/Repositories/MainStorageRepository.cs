using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Linq;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class MainStorageRepository : IMainStorageRepository
    {
        private readonly ApplicationDbContext _db;

        public MainStorageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public MainStorage GetMainStorage()
        {
            return _db.MainStorages.SingleOrDefault(ms => ms.ID == 1);
        }
    }
}