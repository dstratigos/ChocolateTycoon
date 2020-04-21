using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChocolateTycoon.Repositories
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