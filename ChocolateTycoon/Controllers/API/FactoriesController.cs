using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ChocolateTycoon.DTOs;
using AutoMapper;
using System.Web.Http;
using ChocolateTycoon.Data;

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
        public IEnumerable<FactoryDto> GetFactories()
        {
            return db.Factories.Include(f => f.Supplier)
            .ToList()
            .Select(Mapper.Map<Factory, FactoryDto>);
        }

        // PUT: /api/factories/id
        [HttpPut]
        public IHttpActionResult BreakContract(int id)
        {
            var factory = db.Factories.Include(f => f.Supplier).Single(f => f.ID == id);

            factory.BreakContract();

            db.SaveChanges();

            return Ok();
        }


    }
}