using System.ComponentModel.DataAnnotations;

namespace ChocolateTycoon.Core.Models
{
    public enum MessageEnum
    {
        // error messages
        [Display(Name = "This Factory already exists! You are now in Edit Mode!")]
        FactoryExistsError = 1,

        [Display(Name = "This employee already exists! You are now in Edit Mode!")]
        EmployeeExistsError,

        [Display(Name = "Storage Unit not available!")]
        StorageUnitNullError,

        [Display(Name = "Factory reached it's maximum Production capacity for today!")]
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

        [Display(Name = "Not enough chocolate stock. Please restock!")]
        SellStockError,

        [Display(Name = "You'll have to wait until tomorrow to do that!")]
        ActionNotAllowedError,

        // info messages
        [Display(Name = "chocolates stored")]
        ProducedInfo,

        [Display(Name = "chocolates sent to charity!")]
        CharityInfo,

        [Display(Name = "Done!")]
        SellInfo
    }
}