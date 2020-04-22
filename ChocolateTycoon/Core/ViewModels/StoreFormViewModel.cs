using ChocolateTycoon.Controllers;
using ChocolateTycoon.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ChocolateTycoon.Core.ViewModels
{
    public class StoreFormViewModel
    {
        public int ID { get; set; }
        public Store Store { get; set; }
        public decimal Earnings { get; set; }
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
            Store = store;
        }

        public StoreFormViewModel()
        {  }
    }
}