using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response;
using PE_PRN232_FA25_AnhTHQ_BLL.Services;
using PE_PRN232_FA25_AnhTHQ_DAL.Models;
using PE_PRN232_FA25_AnhTHQ_DAL.Repostitories;
using PRN232_SU25_SE182634.api.Middlewares;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var firstError = context.ModelState
            .Where(kvp => kvp.Value?.Errors.Count > 0)
            .Select(kvp => $"{kvp.Key}: {kvp.Value!.Errors.First().ErrorMessage}")
            .FirstOrDefault() ?? "Missing/Invalid input";
        var body = ApiResponse<string>.Error(ApiStatusCode.HB40001, firstError);
        return new BadRequestObjectResult(body);
    };
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Web API",
        Version = "v1",
        Description = "API documentation for  project"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Paste access token only (no 'Bearer ' prefix)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtMinutes = int.TryParse(builder.Configuration["Jwt:AccessTokenMinutes"], out var m) ? m : 60;

// Authentication + JWT bearer
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey ?? string.Empty)),
            RoleClaimType = ClaimTypes.Role
        };

        // Accept raw token without "Bearer " prefix
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                var auth = ctx.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrWhiteSpace(auth))
                {
                    if (auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        ctx.Token = auth.Substring("Bearer ".Length).Trim();
                    else
                        ctx.Token = auth.Trim();
                }
                return Task.CompletedTask;
            },
            OnChallenge = async ctx =>
            {
                ctx.HandleResponse();
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                ctx.Response.ContentType = "application/json";
                var payload = System.Text.Json.JsonSerializer.Serialize(
                    ApiResponse<string>.Error(ApiStatusCode.HB40101, "Token missing/Invalid"));
                await ctx.Response.WriteAsync(payload);
            },
            OnForbidden = async ctx =>
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                ctx.Response.ContentType = "application/json";
                var payload = System.Text.Json.JsonSerializer.Serialize(
                    ApiResponse<string>.Error(ApiStatusCode.HB40301, "Permission Denied"));
                await ctx.Response.WriteAsync(payload);
            }
        };
    });

builder.Services.AddDbContext<FA25BearDBContext>();
builder.Services.AddScoped<BearAccountRepos>();
builder.Services.AddScoped<BearProfileRepos>();

builder.Services.AddScoped(_ => new JwtService(jwtIssuer!, jwtAudience!, jwtKey!, jwtMinutes));
builder.Services.AddScoped<BearAccountService>();
builder.Services.AddScoped<BearProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Only redirect to HTTPS in Production to keep HTTP working during local dev
if (app.Environment.IsProduction())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == StatusCodes.Status404NotFound &&
        !response.HasStarted)
    {
        response.ContentType = "application/json";
        var payload = System.Text.Json.JsonSerializer.Serialize(
            ApiResponse<string>.Error(ApiStatusCode.HB40401, "Resource not found"));
        await response.WriteAsync(payload);
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();