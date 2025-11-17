using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PE_PRN232_FA25_VuThanhAn_BE;
using Repository.Models;
using Service;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

static IEdmModel GetEdmModel()
{
	var odataBuilder = new ODataConventionModelBuilder();
	odataBuilder.EntitySet<BearProfile>("BearProfile"); // EDM - ENTITY DATA MODEL
	odataBuilder.EntitySet<BearType>("BearType");
	return odataBuilder.GetEdmModel();
}

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
	})
	.AddOData(opt =>
	{
		opt.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count();
		opt.AddRouteComponents("odata", GetEdmModel());
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
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
			// ClockSkew = TimeSpan.Zero
		};
		options.Events = new JwtBearerEvents
		{
			OnChallenge = context =>
			{
				context.HandleResponse(); // Skip default response
				context.Response.StatusCode = 401;
				return context.Response.WriteAsJsonAsync(ErrorCodeModel.Unauthor());
			},
			OnForbidden = context =>
			{
				context.Response.StatusCode = 403;
				return context.Response.WriteAsJsonAsync(ErrorCodeModel.Deny());
			}
		};
	});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
	////JWT Config
	option.DescribeAllParametersInCamelCase();
	option.ResolveConflictingActions(conf => conf.First());     // duplicate API name if any, ex: Get() & Get(string id)
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
	option.DocInclusionPredicate((docName, apiDesc) =>
	{
		var relativePath = apiDesc.RelativePath;
		return relativePath == null || !relativePath.StartsWith("odata");
	});
});

builder.Services.AddScoped<IBearAccountService, BearAccountService>();
builder.Services.AddScoped<IBearProfileService, BearProfileService>();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
