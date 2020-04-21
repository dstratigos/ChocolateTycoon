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

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Store Name should contain at least 3 charactes.")]
        public string Name { get; set; }

        public byte Level { get; private set; }

        public int MainStorageID { get; set; } = 1;

        public int SafeID { get; set; } = 1;

        public MainStorage MainStorage { get; set; }

        public Safe Safe { get; set; }

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
            MainStorageID = store.MainStorageID;
            SafeID = store.SafeID;
        }

        public StoreFormViewModel()
        {  }
    }
}