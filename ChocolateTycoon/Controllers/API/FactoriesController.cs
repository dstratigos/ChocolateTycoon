using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ChocolateTycoon.DTOs;
using AutoMapper;
using System.Web.Http;

namespace ChocolateTycoon.Controllers.API
{
    public class FactoriesController : ApiController
    {
        ApplicationDbContext db;

        public FactoriesController()
        {
            db = new ApplicationDbContext();
        }

        // GET: api/factories
        public IEnumerable<FactoryDTO> GetFactories()
        {
            return db.Factories
            .Include(f => f.ProductionUnit)
            .Include(f => f.StorageUnit)
            .ToList()
            .Select(Mapper.Map<Factory, FactoryDTO>);
        }


    }
}