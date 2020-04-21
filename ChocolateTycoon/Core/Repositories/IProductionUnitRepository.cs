using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface IProductionUnitRepository
    {
        IEnumerable<ProductionUnit> GetProductionUnits();
        void Add(ProductionUnit productionUnit);
    }
}