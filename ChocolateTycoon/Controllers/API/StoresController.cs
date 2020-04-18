using ChocolateTycoon.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using System.Net;
using ChocolateTycoon.Data;
using System.Data.Entity;
using ChocolateTycoon.Persistence;
using ChocolateTycoon.ViewModels;

namespace ChocolateTycoon.Models.API
{
    public class StoresController : ApiController
    {
        private readonly ApplicationDbContext db;
        private readonly UnitOfWork unitOfWork;

        public StoresController()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(db);
        }

        // GET api/stores
        public IEnumerable<StoreDTO> GetStores()
        {
            return db.Stores.ToList()
                .Select(Mapper.Map<Store, StoreDTO>);
        }

        // GET /api/stores/id
        public IHttpActionResult IndexAPI(int id)
        {
            var serie = db.Stores.SingleOrDefault(s => s.ID == id);

            if (serie == null)
                return NotFound();

            return Ok(Mapper.Map<Store, StoreDTO>(serie));
        }

        // POST /api/stores
        [HttpPost]
        public IHttpActionResult CreateStore(StoreDTO storeDto)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var store = Mapper.Map<StoreDTO, Store>(storeDto);

            db.Stores.Add(store);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + store.ID), storeDto);
        }

        // PUT /api/stores/id
        [HttpPut]
        public void UpdateStore(int id, StoreDTO storeDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var storeDb = db.Stores.SingleOrDefault(s => s.ID == id);

            if (storeDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(storeDto, storeDb);

            db.SaveChanges();
        }

        // DELETE api/stores/id
        [HttpDelete]
        public void DeleteStore(int id)
        {
            var storeDb = db.Stores
                .Include(s => s.Safe)
                .SingleOrDefault(s => s.ID == id);

            if (storeDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            storeDb.Safe.Refund(Store.CreateCost);

            db.Stores.Remove(storeDb);
            db.SaveChanges();
        }
    }
}