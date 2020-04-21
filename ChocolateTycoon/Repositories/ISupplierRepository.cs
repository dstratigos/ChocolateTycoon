using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateTycoon.Repositories
{
    public interface ISupplierRepository
    {
        Supplier GetSupplier(int id);
        Supplier GetSupplierWithFactories(int id);
    }
}
