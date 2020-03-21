using AutoMapper;
using ChocolateTycoon.DTOs;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.App_Start
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Store, StoreDTO>();
            CreateMap<StoreDTO, Store>();

            CreateMap<Factory, FactoryDto>();
            CreateMap<FactoryDto, Factory>();

            CreateMap<ProductionUnit, ProductionUnitDto>();
            CreateMap<ProductionUnitDto, ProductionUnit>();

            CreateMap<StorageUnit, StorageUnitDto>();
            CreateMap<StorageUnitDto, StorageUnit>();
        }
    }
}