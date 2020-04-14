using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Employee> GetEmployeesBasedOnStore(int storeId)
        {
            return _db.Employees
                .Where(e => e.StoreID == storeId);
        }
    }
}