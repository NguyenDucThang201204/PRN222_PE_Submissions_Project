using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PE_PRN232_FA25_LeCongHung_BLL.Services;
using PE_PRN232_FA25_LeCongHung_DAL.Data;
using PE_PRN232_FA25_LeCongHung_DAL.Repositories;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BearDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Repository and UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure Services
builder.Services.AddScoped<IBearProfileService, BearProfileService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured");
var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured");
var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured");

builder.Services.AddScoped<IAccountService>(provider =>
{
    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    return new AccountService(unitOfWork, secretKey, issuer, audience);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        // Map role claim correctly
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddAuthorization(options =>
{
    // Manager role (RoleId = 1) - has all permissions
    // Can use either Role claim or RoleId claim
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
});

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bear Pet Management API",
        Version = "v1",
        Description = "API for managing bear profiles and accounts"
    });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Always enable Swagger for API testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bear Pet Management API v1");
});

// Allow both HTTP and HTTPS to work without redirection
// app.UseHttpsRedirection(); // Commented out to allow both protocols

// Use CORS
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

