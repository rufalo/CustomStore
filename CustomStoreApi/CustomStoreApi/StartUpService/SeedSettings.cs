using CustomStoreApi.Context;
using CustomStoreApi.Context.Tabels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.StartUpService
{
    public static class SeedSettings
    {
        public static void ConfigureSeedSettings(this IServiceCollection service)
        {
            var provder = service.BuildServiceProvider();
            var userManager = provder.GetService<UserManager<ApplicationUser>>();
            var roleManager = provder.GetService<RoleManager<IdentityRole>>();
            var context = provder.GetService<ApplicationContext>();

            if (context.Database.EnsureCreated())
            {
                SeedRoles(roleManager);
                SeedUser(userManager, context);
            }

            
        }      

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string NormalRole = "Normal";
            if (!roleManager.RoleExistsAsync(NormalRole).Result)
            {
                var role = new IdentityRole()
                {
                    Name = NormalRole
                };
                roleManager.CreateAsync(role);
            }

            string AdminRole = "Adminstrator";
            if (!roleManager.RoleExistsAsync(AdminRole).Result)
            {
                var role = new IdentityRole()
                {
                    Name = AdminRole
                };
                roleManager.CreateAsync(role);
            }
        }

        private static async void SeedUser(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            if (userManager.FindByNameAsync("testUser1").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "testUser1",
                    Email = "testUser1@example.com"
                };
                var result = await userManager.CreateAsync(user, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Normal");
                }
            }

            if (userManager.FindByNameAsync("adminOne1").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "adminOne1",
                    Email = "adminOne1@example.com"
                };
                var result = await userManager.CreateAsync(user, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Normal");
                    await userManager.AddToRoleAsync(user, "Adminstrator");
                }
            }
        }
    }
}
