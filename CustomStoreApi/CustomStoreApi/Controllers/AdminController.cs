using CustomStoreApi.Context;
using CustomStoreApi.Context.Tabels;
using CustomStoreApi.DTO;
using CustomStoreApi.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.Controllers
{
    public class AdminController : ControllerBase
    {
        private OffersQueries _OffersQueries;
        private UserManager<ApplicationUser> _UserManager;

        public AdminController(ApplicationContext context ,UserManager<ApplicationUser> userManager)
        {
            _OffersQueries = new OffersQueries(context);
            _UserManager = userManager;
        }
        public IActionResult GetAllUsers()
        {
            var result = _UserManager.Users.Select(x => new UserDTO() {Id = x.Id, Name = x.UserName });
            return Ok();
        }

        public IActionResult GetAllOffers()
        {
            return Ok(_OffersQueries.GetAllOffers());
        }

        public IActionResult EditOffer([FromBody]Offer offer)
        {
            var result = _OffersQueries.EditOffer(offer);
            if (result == null)
                return BadRequest("Could Not Edit Offer");
            return Ok(result);
        }

        public IActionResult DeleteOffer([FromBody]Offer offer)
        {
            _OffersQueries.DeleteOffer(offer);
            return Ok();
        }
    }
}
