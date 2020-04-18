using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Repositories
{
    public class ProductionUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductionUnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(ProductionUnit productionUnit)
        {
            _db.ProductionUnits.Add(productionUnit);
        }
    }
}