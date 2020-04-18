using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Repositories
{
    public class StorageUnitRepository
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