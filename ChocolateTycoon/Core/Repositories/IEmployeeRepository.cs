using ChocolateTycoon.Core.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesBasedOnStore(int storeId);
        IEnumerable<Employee> GetEmployeesWithStoresAndFactories();
        void Add(Employee employee);
        void AddMany(List<Employee> employees);
        void Remove(Employee employee);
        void RemoveMany(List<Employee> employees);
    }
}