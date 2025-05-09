using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL
{
    public class BookingRepository
    {
        private readonly CarRentalDbContext _context;

        public BookingRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllBookingAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.BookingId == id);
        }

        public async Task<List<Booking>> GetBookingsByCustomerNameAsync(string fullName)
        {
            return await _context.Bookings
                                 .Include(b => b.Customer)
                                 .Where(b => b.Customer != null &&
                                             b.Customer.FullName.ToLower().Contains(fullName.ToLower()))
                                 .ToListAsync();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            try
            {
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving booking: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            Booking? existingBooking = await _context.Bookings.FindAsync(booking.BookingId);
            if (existingBooking != null)
            {
                Console.WriteLine($"Updating booking {booking.BookingId} in database.");

                existingBooking.StartDate = booking.StartDate;
                existingBooking.EndDate = booking.EndDate;
                existingBooking.Status = booking.Status;

                _context.Bookings.Update(existingBooking);
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Booking not found for update.");
            }
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            Booking? booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
