using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.ViewModels
{
    public class StoreFormViewModel
    {
        public int? ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte Level { get; set; }

        public List<Chocolate> DbChocolates { get; set; }

        public string Title
        {
            get
            {
                return ID != 0 ? "Edit Store" : "New Store";
            }
        }

        public StoreFormViewModel()
        {
            ID = 0;
        }

        public StoreFormViewModel(Store store)
        {
            ID = store.ID;
            Name = store.Name;
            Level = store.Level;
            DbChocolates = store.Chocolates;
        }
    }
}