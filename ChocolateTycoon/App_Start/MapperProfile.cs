using AutoMapper;
using ChocolateTycoon.Core.DTOs;
using ChocolateTycoon.Core.Models;

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
        }
    }
}