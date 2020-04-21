using ChocolateTycoon.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Repositories
{
    public interface IChocolateRepository
    {
        void Add(List<Chocolate> chocolates);
        IEnumerable<Chocolate> GetMainStorageChocolates();
        IEnumerable<Chocolate> GetStoreChocolates(int storeId);
    }
}