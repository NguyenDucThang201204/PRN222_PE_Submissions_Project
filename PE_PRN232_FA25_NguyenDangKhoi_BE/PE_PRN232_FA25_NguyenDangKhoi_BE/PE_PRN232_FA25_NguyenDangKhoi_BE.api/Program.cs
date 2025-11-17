using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PE_PRN232_FA25_NguyenDangKhoi.api.Configuration;
using PE_PRN232_FA25_NguyenDangKhoi.api.ExceptionHandler;
using Repositories;
using Repositories.Models;
using Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// OData EDM Model
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<BearProfile>("BearProfile");
modelBuilder.EntitySet<BearType>("BearType");


builder.Services
    .AddControllers()
    .AddOData(options =>
    {
        options
            .Select().Filter().OrderBy().Expand().SetMaxTop(null).Count()
            .AddRouteComponents("odata", modelBuilder.GetEdmModel());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var msg = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input" : e.ErrorMessage)
                .FirstOrDefault() ?? "Missing/invalid input";

            return new BadRequestObjectResult(new { errorCode = "HB40001", message = msg });
        };
    });

builder.Services.AddDbContext<BearManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ** Add scoped services **
builder.Services.AddScoped<BearProfileRepository>();
builder.Services.AddScoped<BearTypeRepository>();
builder.Services.AddScoped<AccountsRepository>();
builder.Services.AddScoped<AccountsService>();
builder.Services.AddScoped<BearProfileService>();
builder.Services.AddScoped<BearTypeService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.DescribeAllParametersInCamelCase();
    option.ResolveConflictingActions(conf => conf.First());

    option.OperationFilter<ODataQueryOptionsOperationFilter>();

    option.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(DocExpansion.None);   
        c.DefaultModelsExpandDepth(-1);        
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
