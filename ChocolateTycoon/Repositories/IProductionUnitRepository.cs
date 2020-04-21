using ChocolateTycoon.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Repositories
{
    public interface IProductionUnitRepository
    {
        IEnumerable<ProductionUnit> GetProductionUnits();
        void Add(ProductionUnit productionUnit);
    }
}