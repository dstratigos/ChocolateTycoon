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
            var factory = db.Factories
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier)
                .Single(f => f.ID == id);

            var vault = db.Safes.SingleOrDefault();

            if (factory == null)
                return BadRequest();

            //safe.BreakContractPenalty(factory.StorageUnit, factory.Supplier);

            var message = factory.BreakContract(vault);

            db.SaveChanges();

            return Ok(message);
        }

        // POST: /api/factories/id
        [HttpPost]
        public IHttpActionResult MakeContract(SuppliedFactoryDto suppliedFactory)
        {
            var factories = db.Factories
                .Include(f => f.StorageUnit)
                .Include(f => f.Supplier)
                .ToList();
            var contractFactory = factories.Find(f => f.ID == suppliedFactory.Id);
            var supplier = db.Suppliers.SingleOrDefault(s => s.Id == suppliedFactory.supplierId);

            var message = "";

            if (factories == null || supplier == null)
                return BadRequest();

            else if (contractFactory.StorageUnit == null)
            {
                message = $"{contractFactory.Name} does not have a Storage Unit. Create one first!";
                return BadRequest(message);
            }

            else if (Factory.HasActiveContract(factories, contractFactory.ID))
            {
                message = $"{contractFactory.Name} already has an active Contract. Break that Contract first.";
                return BadRequest(message);
            }

            message = contractFactory.MakeContract(supplier);

            db.SaveChanges();

            return Ok(message);
        }


    }
}