﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public enum MessageEnum
    {
        // error messages
        [Display(Name = "Storage Unit not available!")]
        StorageUnitNullError = 1,

        [Display(Name = "Factory reached it's maximum Production for today!")]
        ProductionTurnError,

        [Display(Name = "Supplier's quota has been reached. Contract terminated.")]
        SupplierQuotaError,

        [Display(Name = "Make a Contract with a Supplier first!")]
        NoSupplierError,

        [Display(Name = "Make some money first!")]
        NotEnoughMoneyError,

        [Display(Name = "Hire some people first!")]
        PersonelError,

        [Display(Name = "Replenish the Storage Unit first!")]
        RawMaterialsError,

        // info messages
        [Display(Name = "chocolates stored")]
        ProducedInfo,

        [Display(Name = "chocolates sent to charity!")]
        CharityInfo
    }
}