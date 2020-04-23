using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface IStoreRepository
    {
        void Add(Store store);
        Store GetStore(int id);
        IEnumerable<Store> GetStores();
        IEnumerable<Store> GetStoresWithAllDetails();
        Store GetStoreWithAllDetails(int id);
        Store GetStoreWithSafe(int id);
        void Remove(Store store);
        void RemoveMany(List<Store> stores);
    }
}