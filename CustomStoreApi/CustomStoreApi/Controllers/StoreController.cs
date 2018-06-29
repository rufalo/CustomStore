using CustomStoreApi.Context;
using CustomStoreApi.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.Controllers
{
    public class StoreController : ControllerBase
    {
        private readonly OffersQueries _OffersQueries;

        public StoreController(ApplicationContext context)
        {
            _OffersQueries = new OffersQueries(context);
        }

        /// <summary>
        /// Get The Standard Offers That are Valid, if the Item is .Valid
        /// </summary>
        /// <returns></returns>
        public IActionResult Offers()
        {
            return Ok(_OffersQueries.GetAllVisableOffers());
        }
    }
}
