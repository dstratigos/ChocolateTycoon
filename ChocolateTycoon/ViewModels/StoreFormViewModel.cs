using ChocolateTycoon.Controllers;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.ViewModels
{
    public class StoreFormViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public byte Level { get; }

        public int Stock { get; }

        public int MainStorageID { get; }

        public int SafeID { get; }

        public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public List<Chocolate> Chocolates { get; }

        public string Message { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<StoreController, ActionResult>> update = (s => s.Update(this));

                Expression<Func<StoreController, ActionResult>> create = (c => c.Create(this));

                var action = (ID != 0) ? update : create;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        public StoreFormViewModel(Store store)
        {
            ID = store.ID;
            Name = store.Name;
            Level = store.Level;
            Stock = store.Stock;
            MainStorageID = store.MainStorageID;
            SafeID = store.SafeID;
            Employees = store.Employees;
            Chocolates = store.Chocolates;
        }

        public StoreFormViewModel()
        {

        }
    }
}