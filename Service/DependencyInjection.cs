using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(
            configuration.GetSection("Jwt"));

            services.AddScoped<TokenService>();
            services.AddScoped<BearAccountService>();
            services.AddScoped<BearProfileService>();
            //services.AddScoped<>();
        }
    }
}
