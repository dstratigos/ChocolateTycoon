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
            var suppliers = db.Suppliers.ToList();            

            var suppliersDTO = new List<SupplierDto>();

            foreach (var supplier in suppliers)
            {
                var supplierDTO = new SupplierDto
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
            var supplierDb = db.Suppliers.Include(s => s.Factories.Where(f => f.SupplierId == id)).Single(s => s.Id == id);
            


            return Ok(supplierDb);
        }
    }
}
