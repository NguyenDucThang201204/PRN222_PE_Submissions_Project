using PE_PRN232_FA25_PhamTrungHieu_BE.API.Extensions;
using PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.Repositories;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            _ = builder.Services.AddCustomSwagger();
            _ = builder.Services.AddConfigureJwt(builder.Configuration);

            _ = builder.Services.AddScoped(typeof(GenericRepository<>));
            _ = builder.Services.AddScoped<IBearAccountService, BearAccountService>();
            _ = builder.Services.AddScoped<IBearProfileService, BearProfileService>();

            _ = builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI();
            }

            _ = app.UseHttpsRedirection();

            _ = app.UseHandleServerErrorMiddleware();

            _ = app.UseAuthentication();

            _ = app.UseAuthorization();

            _ = app.MapControllers();

            app.Run();
        }
    }
}
