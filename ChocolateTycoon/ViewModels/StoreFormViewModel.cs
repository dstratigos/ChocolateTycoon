using ChocolateTycoon.Controllers;
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
    }

}