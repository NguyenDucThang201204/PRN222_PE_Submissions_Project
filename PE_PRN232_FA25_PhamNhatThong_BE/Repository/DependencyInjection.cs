using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Fa25bearDbContext>(options =>
            options.UseSqlServer(connectionString));
            services.AddScoped<AccountRepository>();
            services.AddScoped<BearRepository>();
            return services;
        }
    }
}
