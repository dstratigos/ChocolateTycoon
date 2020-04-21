using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Collections.Generic;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class ProductionUnitRepository : IProductionUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductionUnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductionUnit> GetProductionUnits()
        {
            return _db.ProductionUnits;
        }

        public void Add(ProductionUnit productionUnit)
        {
            _db.ProductionUnits.Add(productionUnit);
        }
    }
}