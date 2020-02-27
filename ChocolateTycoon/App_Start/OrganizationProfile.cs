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

            CreateMap<Factory, FactoryDTO>();
            CreateMap<FactoryDTO, Factory>();

            CreateMap<ProductionUnit, ProductionUnitDTO>();
            CreateMap<ProductionUnitDTO, ProductionUnit>();

            CreateMap<StorageUnit, StorageUnitDTO>();
            CreateMap<StorageUnitDTO, StorageUnit>();
        }
    }
}