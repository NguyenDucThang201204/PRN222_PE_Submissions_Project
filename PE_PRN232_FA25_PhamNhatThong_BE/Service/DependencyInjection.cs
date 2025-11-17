using Microsoft.Extensions.DependencyInjection;
using Service.Security;


namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<JwtHelper>();
            services.AddScoped<AuthService>();
            services.AddScoped<BearService>();
            return services;
        }
    }
}
