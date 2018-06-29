using CustomStoreApi.Context.Tabels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomStoreApi.Authentication
{
    public class AuthTool
    {
        public static async Task<ApplicationUser> CeckIfValidUser(HttpContext httpContext, UserManager<ApplicationUser> userManager)
        {
            // Cast it to  CalimsIdentity To be able to Use .FindFyrst To get SecurityStamp
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var user = await userManager.FindByNameAsync(identity.Name);
            if (user == null)
                return null;

            // Check if SecurityStamp Is OK
            var securityStamp = identity.FindFirst("securityStamp").Value;
            if (user.SecurityStamp != securityStamp)
                return null;

            return user;
        }
    }
}
