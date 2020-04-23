using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface IChocolateRepository
    {
        void AddMany(List<Chocolate> chocolates);
        void RemoveMany(List<Chocolate> chocolates);
        IEnumerable<Chocolate> GetChocolates();
        IEnumerable<Chocolate> GetMainStorageChocolates();
        IEnumerable<Chocolate> GetStoreChocolates(int storeId);        
    }
}