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
        public IHttpActionResult GetFactories()
        {
            var factories = db.Factories.Include(f => f.Supplier).ToList();

            var factoriesDto = new List<FactoryDto>();
            
            return Ok(Mapper.Map(factories, factoriesDto));
        }

        // PUT: /api/factories/id
        [HttpPut]
        public IHttpActionResult BreakContract(int id)
        {
            var factory = db.Factories.Include(f => f.Supplier).Single(f => f.ID == id);

            if (factory == null)
                return BadRequest();

            var message = factory.BreakContract();

            db.SaveChanges();

            return Ok(message);
        }

        // POST: /api/factories/id
        [HttpPost]
        public IHttpActionResult MakeContract(SuppliedFactoryDto suppliedFactory)
        {
            var factory = db.Factories.Include(f => f.Supplier).SingleOrDefault(f => f.ID == suppliedFactory.Id);
            var supplier = db.Suppliers.SingleOrDefault(s => s.Id == suppliedFactory.supplierId);

            if (factory == null || supplier == null)
                return BadRequest();

            var message = factory.MakeContract(supplier);

            db.SaveChanges();

            return Ok(message);
        }


    }
}