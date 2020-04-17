using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Repositories
{
    public class MainStorageRepository
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