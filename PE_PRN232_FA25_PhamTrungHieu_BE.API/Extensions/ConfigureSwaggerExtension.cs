using Microsoft.OpenApi.Models;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.API.Extensions
{
    public static class ConfigureSwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            _ = services.AddEndpointsApiExplorer();
            _ = services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LeopardProfile API",
                    Version = "v1"
                });

                // Config jwt in Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var relativePath = apiDesc.RelativePath;
                    return relativePath == null || !relativePath.StartsWith("odata");
                });
            });

            return services;
        }
    }
}
