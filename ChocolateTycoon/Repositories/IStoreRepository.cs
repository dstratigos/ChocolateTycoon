using ChocolateTycoon.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Repositories
{
    public interface IStoreRepository
    {
        void Add(Store store);
        Store GetStore(int id);
        IEnumerable<Store> GetStores();
        Store GetStoreWithAllDetails(int id);
        Store GetStoreWithSafe(int id);
        void Remove(Store store);
    }
}