using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Store
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte Level { get; set; }

        //private int _mainStorageID;

        //public int MainStorageID
        //{
        //    get => _mainStorageID;

        //    private set => _mainStorageID = 1;
        //}

        //private int _safeID;

        //public int SafeID
        //{
        //    get => _safeID;

        //    private set => _safeID = 1;
        //}

        //public MainStorage MainStorage { get; set; }

        //public Safe Safe { get; set; }

        public List<Chocolate> Chocolates { get; set; }
    }
}