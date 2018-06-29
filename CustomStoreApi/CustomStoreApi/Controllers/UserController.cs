using CustomStoreApi.Authentication;
using CustomStoreApi.Context.Tabels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomStoreApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly IConfiguration _Configuration;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("ModelState Invalid");

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Now To Add Roles
                await _UserManager.AddToRoleAsync(user, "Normal");

                // Login
                return Login(new LoginModel { UserName = model.UserName,Password = model.Password }).Result;
            }

            return BadRequest("Could Not Create User");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {            
            var result = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _UserManager.FindByNameAsync(model.UserName);
                var roles = await _UserManager.GetRolesAsync(user);
                var tokenGenerator = new TokenGenerator(_Configuration);
                string token = tokenGenerator.CreateTokenFin(user, roles.ToList());
                return Ok(token);
            }
            else
            {
                return BadRequest("Could Not Login");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = await AuthTool.CeckIfValidUser(HttpContext, _UserManager);
            if (user == null)
                return BadRequest("Invalid User");
            await _UserManager.UpdateSecurityStampAsync(user);
            return Ok("done");

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Private()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = await AuthTool.CeckIfValidUser(HttpContext, _UserManager);
            if (user != null)
                return Ok("Private Data");
            else
                return BadRequest("Forbidden");

        }

    }


    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
