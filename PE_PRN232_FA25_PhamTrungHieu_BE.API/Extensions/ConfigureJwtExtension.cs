using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.API.Extensions
{
    public static class ConfigureJwtExtension
    {
        public static IServiceCollection AddConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["JwtSettings:SecretKey"];
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];

            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = UTF8Encoding.UTF8.GetBytes(secretKey ?? string.Empty);
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        return context.Response.WriteAsJsonAsync(ErrorResponse.Unauthor());
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        return context.Response.WriteAsJsonAsync(ErrorResponse.Deny());
                    }
                };
            });

            _ = services.AddAuthorization();

            return services;
        }
    }
}
