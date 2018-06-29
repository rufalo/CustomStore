using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.StartUpService
{
    public static class CorsSettings
    {
        public static void ConfigureCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("PaymentPolicy",
                    builder => builder.AllowAnyOrigin()
                    .WithMethods("Get", "put", "Post")
                    .AllowAnyHeader()
                    );
            });
        }
    }
}
