using ChocolateTycoon.Data;
using ChocolateTycoon.Persistence;
using System.Net;
using System.Web.Http;

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
            var storeDb = unitOfWork.Stores.GetStoreWithAllDetails(id);

            if (storeDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            storeDb.Safe.Refund(Store.CreateCost);

            unitOfWork.Stores.Remove(storeDb);
            unitOfWork.Complete();
        }
    }
}