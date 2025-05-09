using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL
{
    public class CarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<List<Car>> GetCarsByMakeAsync(string make)
        {
            return await _context.Cars
                                 .Where(c => c.Make.ToLower().Contains(make.ToLower()))
                                 .ToListAsync();
        }

        public async Task AddCarAsync(Car car)
        {
            try
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error while saving car data.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the car.", ex);
            }
        }

        public async Task UpdateCarAsync(Car car)
        {
            Car? existingCar = await _context.Cars.FindAsync(car.CarId);
            if (existingCar != null)
            {
                Console.WriteLine($"Updating car {car.CarId} in database.");

                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.LicensePlate = car.LicensePlate;
                existingCar.Status = car.Status;
                existingCar.RentalPricePerDay = car.RentalPricePerDay;

                _context.Cars.Update(existingCar);
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Car not found for update.");
            }
        }

        public async Task DeleteCarAsync(int carId)
        {
            Car? car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
