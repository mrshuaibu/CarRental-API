
using CarRental.BLL;
using CarRental.DAL;
using CarRental.Mappings;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarRental
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(CarProfile), typeof(CustomerProfile), typeof(BookingProfile));

            builder.Services.AddDbContext<CarRentalDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register BLL Services
            builder.Services.AddTransient<BookingService>();
            builder.Services.AddTransient<CarService>();
            builder.Services.AddTransient<CustomerService>();
            // Register DAL Services
            builder.Services.AddTransient<BookingRepository>();
            builder.Services.AddTransient<CarRepository>();
            builder.Services.AddTransient<CustomerRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
