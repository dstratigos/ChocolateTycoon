using ChocolateTycoon.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using System.Net;
using ChocolateTycoon.Data;

namespace ChocolateTycoon.Models.API
{
    public class StoresController : ApiController
    {
        private ApplicationDbContext db;

        public StoresController()
        {
            db = new ApplicationDbContext();
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
            var storeDb = db.Stores.SingleOrDefault(s => s.ID == id);

            if (storeDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            db.Stores.Remove(storeDb);
            db.SaveChanges();
        }
    }
}