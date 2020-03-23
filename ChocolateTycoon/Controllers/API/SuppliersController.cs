using AutoMapper;
using ChocolateTycoon.Data;
using ChocolateTycoon.DTOs;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChocolateTycoon.Controllers.API
{
    public class SuppliersController : ApiController
    {
        private ApplicationDbContext db;

        public SuppliersController()
        {
            db = new ApplicationDbContext();
        }

        // GET: /Api/Suppliers/
        [HttpGet]
        public IHttpActionResult GetSuppliers()
        {
            var suppliers = db.Suppliers.Include(s => s.Factories).ToList();

            var suppliersDto = new List<SupplierDto>();

            suppliersDto = Mapper.Map<List<SupplierDto>>(suppliers);
            
            return Ok(suppliersDto);
        }

        // GET: /Api/Suppliers/Id
        [HttpGet]
        public IHttpActionResult GetSupplier(int id)
        {
            var supplierDb = db.Suppliers.Include(s => s.Factories).Single(s => s.Id == id);

            return Ok(Mapper.Map<SupplierDto>(supplierDb));
        }

        // PUT: /api/suppliers
        [HttpPut]
        public IHttpActionResult BreakContract(SupplierDto supplierDto)
        {
            return Ok();
        }
    }
}
