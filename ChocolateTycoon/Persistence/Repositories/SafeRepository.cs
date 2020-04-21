using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System.Linq;

namespace ChocolateTycoon.Repositories
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