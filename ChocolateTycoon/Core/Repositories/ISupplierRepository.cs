using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplier(int id);
        Supplier GetSupplierWithFactories(int id);
    }
}
