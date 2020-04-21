using ChocolateTycoon.Models;
using System.Collections.Generic;

namespace ChocolateTycoon.Repositories
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesBasedOnStore(int storeId);
        IEnumerable<Employee> GetEmployeesWithStoresAndFactories();
        void Remove(Employee employee);
    }
}