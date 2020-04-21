using ChocolateTycoon.Data;
using ChocolateTycoon.Repositories;

namespace ChocolateTycoon.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IStoreRepository Stores { get; private set; }
        public IFactoryRepository Factories { get; private set; }
        public IMainStorageRepository MainStorages { get; private set; }
        public IProductionUnitRepository ProductionUnits { get; private set; }
        public IStorageUnitRepository StorageUnits { get; private set; }
        public IChocolateRepository Chocolates { get; private set; }
        public ISafeRepository Safes { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public ISupplierRepository Suppliers { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Stores = new StoreRepository(db);
            Factories = new FactoryRepository(db);
            MainStorages = new MainStorageRepository(db);
            ProductionUnits = new ProductionUnitRepository(db);
            StorageUnits = new StorageUnitRepository(db);
            Chocolates = new ChocolateRepository(db);
            Safes = new SafeRepository(db);
            Employees = new EmployeeRepository(db);
            Suppliers = new SupplierRepository(db);
        }

        public void Complete()
        {
            _db.SaveChanges();
        }
    }
}