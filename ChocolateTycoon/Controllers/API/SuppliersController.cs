using AutoMapper;
using ChocolateTycoon.Data;
using ChocolateTycoon.DTOs;
using ChocolateTycoon.Models;
using ChocolateTycoon.Persistence;
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
        private readonly ApplicationDbContext db;
        private readonly UnitOfWork unitOfWork;

        public SuppliersController()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(db);
        }

        // GET: /Api/Suppliers/Id
        [HttpGet]
        public IHttpActionResult GetSupplier(int id)
        {
            var supplierDb = unitOfWork.Suppliers.GetSupplierWithFactories(id);

            return Ok(Mapper.Map<SupplierDto>(supplierDb));
        }
    }
}
