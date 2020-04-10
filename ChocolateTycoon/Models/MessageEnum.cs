using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public enum MessageEnum
    {
        // error messages
        [Display(Name = "Supplier's quota has been reached. Contract terminated.")]
        SupplierQuotaError = 1,

        [Display(Name = "Make a Contract with a Supplier first!")]
        NoSupplierError,

        [Display(Name = "Make some money first!")]
        NotEnoughMoneyError,

        [Display(Name = "Make some money first!")]
        ProducedInfo,

        [Display(Name = "Make some money first!")]
        CharityInfo
    }
}