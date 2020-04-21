using ChocolateTycoon.Core.Repositories;

namespace ChocolateTycoon.Core
{
    public interface IUnitOfWork
    {
        IChocolateRepository Chocolates { get; }
        IEmployeeRepository Employees { get; }
        IFactoryRepository Factories { get; }
        IMainStorageRepository MainStorages { get; }
        IProductionUnitRepository ProductionUnits { get; }
        ISafeRepository Safes { get; }
        IStorageUnitRepository StorageUnits { get; }
        ISupplierRepository Suppliers { get;}
        IStoreRepository Stores { get; }

        void Complete();
    }
}