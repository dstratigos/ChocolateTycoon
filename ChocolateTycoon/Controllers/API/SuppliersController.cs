using ChocolateTycoon.Data;
using ChocolateTycoon.DTOs;
using ChocolateTycoon.Models;
using System;
using System.Collections.Generic;
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
            var suppliers = db.Suppliers.ToList();

            var suppliersDTO = new List<SupplierDTO>();

            foreach (var supplier in suppliers)
            {
                var supplierDTO = new SupplierDTO
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    OfferAmount = supplier.OfferAmount,
                    PricePerKilo = supplier.PricePerKilo,
                    ShippedAmount = supplier.ShippedAmount
                };

                suppliersDTO.Add(supplierDTO);
            }

            if (suppliersDTO.Count == 0)
                return NotFound();

            return Ok(suppliersDTO);
        }

        // GET: /Api/Suppliers/Id
        [HttpGet]
        public IHttpActionResult GetSupplier(int id)
        {
            var supplierDb = db.Suppliers.Single(s => s.Id == id);

            var supplierDTO = new SupplierDTO
            {
                Id = supplierDb.Id,
                Name = supplierDb.Name,
                OfferAmount = supplierDb.OfferAmount,
                PricePerKilo = supplierDb.PricePerKilo,
                ShippedAmount = supplierDb.ShippedAmount
            };

            if (supplierDTO == null)
                return NotFound();

            return Ok(supplierDTO);
        }
    }
}
