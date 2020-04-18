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
        public Store Store { get; set; }
        public string Success { get => "Done!"; }
        public string ErrorEmployees { get => "Not enough employees.."; }
        public string SellErrorStock { get => "Not enough stock. Please restock!"; }
        public string StockError { get => "Already full stock."; }
        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<StoreController, ActionResult>> update = (s => s.Update(this));

                Expression<Func<StoreController, ActionResult>> create = (c => c.Create(this));

                var action = (Store.ID != 0) ? update : create;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        public StoreFormViewModel(Store store)
        {
            Store = store;
        }

        public StoreFormViewModel()
        {  }
    }
}