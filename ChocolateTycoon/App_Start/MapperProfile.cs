using AutoMapper;
using ChocolateTycoon.DTOs;
using ChocolateTycoon.Models;
using ChocolateTycoon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Store, StoreDTO>();
            CreateMap<StoreDTO, Store>();

            CreateMap<Factory, FactoryDto>();
            CreateMap<FactoryDto, Factory>();

            CreateMap<Factory, SuppliedFactoryDto>();
            CreateMap<SuppliedFactoryDto, Factory>();

            CreateMap<ProductionUnit, ProductionUnitDto>();
            CreateMap<ProductionUnitDto, ProductionUnit>();

            CreateMap<StorageUnit, StorageUnitDto>();
            CreateMap<StorageUnitDto, StorageUnit>();

            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();

            CreateMap<Employee, EmployeeFormViewModel>();
            CreateMap<EmployeeFormViewModel, Employee>();
        }
    }
}