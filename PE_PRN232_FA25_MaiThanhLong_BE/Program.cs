using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository.Models;
using Repository;
using Service;
using System.Text.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FA25BearDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<AccountRepo>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ModelRepo>();
builder.Services.AddScoped<ModelService>();




builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
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

builder.Services.AddSwaggerGen(option =>
{
    ////JWT Config
    option.DescribeAllParametersInCamelCase();
    option.ResolveConflictingActions(conf => conf.First());     // duplicate API name if any
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
            new string[]{}
        }
    });
});

static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<BearProfile>("BearProfile");
    return odataBuilder.GetEdmModel();
}
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count();
    options.AddRouteComponents("odata", GetEdmModel());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next();

    // Handle 401 Unauthorized
    if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new { errorCode = "404", message = "Token missing/invalid" };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
    }

    // Handle 403 Forbidden
    else if (context.Response.StatusCode == 403 && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new { errorCode = "403", message = "Permission denied" };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
    }

    // Handle 404 Not Found 
    else if (context.Response.StatusCode == 404 && !context.Response.HasStarted &&
             context.Request.Path.StartsWithSegments("/api"))
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new { errorCode = "404", message = "Resource not found" };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
    }

    // Handle 500 Internal Server Error
    else if (context.Response.StatusCode >= 500 && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new { errorCode = "500", message = "Internal server error" };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
    }
});

//CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//authentication
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
