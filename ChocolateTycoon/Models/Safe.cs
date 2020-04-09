﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Safe
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Deposit { get; set; }



        public bool MoneySuffice(int cost)
        {
            if (Deposit >= cost)
                return true;

            Message.SetErrorMessage(MessageEnum.NotEnoughMoneyError);

            return false;
        }
    }    
}