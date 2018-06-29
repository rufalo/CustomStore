using CustomStoreApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.StartUpService
{
    public static class DbContextSettings
    {
        public static void ConfigureDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("default");
            service.AddDbContext<ApplicationContext>(option => option.UseSqlServer(connection));
        }
    }
}
