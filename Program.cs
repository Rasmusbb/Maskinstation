
using Maskinstation.Data;
using Maskinstation.interfaces;
using Maskinstation.Services;
using Microsoft.EntityFrameworkCore;

namespace Maskinstation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<MaskinstationContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("MachineContext") ?? throw new InvalidOperationException("Connection string 'MachineContext' not found.")));
            builder.Services.AddScoped<Auth>();
            builder.Services.AddScoped<IUser, UserService>();
            builder.Services.AddScoped<IImage, ImageService>();
            builder.Services.AddScoped<IBrand, BrandService>();
            builder.Services.AddScoped<IMachine,MachineService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
