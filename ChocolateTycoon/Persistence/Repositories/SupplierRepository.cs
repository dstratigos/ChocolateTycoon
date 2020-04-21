using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _db;

        public SupplierRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _db.Suppliers;
        }

        public Supplier GetSupplier(int id)
        {
            return _db.Suppliers.SingleOrDefault(s => s.Id == id);
        }

        public Supplier GetSupplierWithFactories(int id)
        {
            return _db.Suppliers.Include(s => s.Factories).Single(s => s.Id == id);
        }
    }
}