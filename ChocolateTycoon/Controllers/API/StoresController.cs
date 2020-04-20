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

        // DELETE api/stores/id
        [HttpDelete]
        public void DeleteStore(int id)
        {
            var storeDb = unitOfWork.Stores.GetStoreWithSafe(id);

            if (storeDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            storeDb.Safe.Refund(Store.CreateCost);

            unitOfWork.Stores.Remove(storeDb);
            unitOfWork.Complete();
        }
    }
}