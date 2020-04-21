using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Linq;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class SafeRepository : ISafeRepository
    {
        private readonly ApplicationDbContext _db;

        public SafeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Safe GetSafe()
        {
            return _db.Safes
                .SingleOrDefault(s => s.ID == 1);
        }
    }
}