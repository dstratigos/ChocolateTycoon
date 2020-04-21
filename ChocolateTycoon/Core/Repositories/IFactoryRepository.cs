using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface IFactoryRepository
    {        
        IEnumerable<Factory> GetFactories();
        IEnumerable<Factory> GetFactoriesWithProductionAndStorageUnit();
        IEnumerable<Factory> GetFactoriesWithStorageUnitAndSupplier();
        IEnumerable<Factory> GetFactoriesWithSupplier();
        Factory GetFactory(int id);
        Factory GetFactoryAllInclusive(int id);
        Factory GetFactoryWithStorageUnitAndSupplier(int id);
        Factory GetFactoryMinusSupplier(int id);
        
        void Add(Factory factory);
        void Remove(Factory factory);
    }
}