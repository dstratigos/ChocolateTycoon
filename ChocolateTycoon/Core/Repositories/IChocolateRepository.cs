using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface IChocolateRepository
    {
        void Add(List<Chocolate> chocolates);
        IEnumerable<Chocolate> GetMainStorageChocolates();
        IEnumerable<Chocolate> GetStoreChocolates(int storeId);
    }
}