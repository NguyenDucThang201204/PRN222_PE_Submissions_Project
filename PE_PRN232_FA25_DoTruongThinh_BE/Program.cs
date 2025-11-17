using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repo.DTO;
using Repo.Models;
using Service;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// --- CORS Configuration ---
var frontendAppUrlHttp = builder.Configuration.GetValue<string>("AllowedOrigins:FrontendHttp");
var frontendAppUrlHttps = builder.Configuration.GetValue<string>("AllowedOrigins:FrontendHttps");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder =>
        {
            var allowedOrigins = new List<string>();
            if (!string.IsNullOrEmpty(frontendAppUrlHttp)) allowedOrigins.Add(frontendAppUrlHttp);
            if (!string.IsNullOrEmpty(frontendAppUrlHttps)) allowedOrigins.Add(frontendAppUrlHttps);

            if (allowedOrigins.Any())
            {
                policyBuilder.WithOrigins(allowedOrigins.ToArray())
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // Crucial for sending cookies cross-origin
            }
            else
            {
                // Fallback for environments where config might be missing, but log a warning.
                var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                logger?.LogWarning("AllowedOrigins not configured. Allowing any origin without credentials for development purposes.");
                policyBuilder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            }
        });
});


static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<BearProfile>("BearProfile"); // EDM - ENTITY DATA MODEL
    odataBuilder.EntitySet<BearType>("BearType");
    return odataBuilder.GetEdmModel();
}
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count();
    options.AddRouteComponents("odata", GetEdmModel());
});
// --- JWT Authentication Configuration ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonSerializer.Serialize(
                    new ErrorResponse("401", "Token missing or invalid"),
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                ));
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonSerializer.Serialize(
                    new ErrorResponse("403", "Permission denied"),
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                ));
            }
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PE PRN API DO TRUONG THINH FA 25", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<BearAccountService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PE PRN API DO TRUONG THINH FA 25"));
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin"); 

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
