using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Repositories
{
    public class SafeRepository
    {
        private readonly ApplicationDbContext _db;

        public SafeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Safe GetSafe()
        {
            return _db.Safes
                .SingleOrDefault();
        }
    }
}