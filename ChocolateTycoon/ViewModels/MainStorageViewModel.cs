using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ChocolateTycoon.Models;

namespace ChocolateTycoon.ViewModels
{
    public class MainStorageViewModel
    {
        public MainStorage MainStorage { get; set; }        
        public IDictionary<string, int> availableStorage = new Dictionary<string, int>();


        public List<Chocolate> Chocolates { get; set; }
        public IDictionary<string, int> availableChocolates = new Dictionary<string, int>();

        public MainStorageViewModel()
        {
            Chocolates = new List<Chocolate>();
        }
    }
}