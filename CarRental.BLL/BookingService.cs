using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.BLL
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(BookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            List<Booking> bookings = await _bookingRepository.GetAllBookingAsync();
            return _mapper.Map<List<BookingDto>>(bookings);
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int bookingId)
        {
            Booking? booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            return booking == null ? null : _mapper.Map<BookingDto>(booking);
        }

        public async Task<List<BookingDto>> GetBookingsByCustomerNameAsync(string fullName)
        {
            List<Booking> bookings = await _bookingRepository.GetBookingsByCustomerNameAsync(fullName);
            return _mapper.Map<List<BookingDto>>(bookings);
        }

        public async Task AddBookingAsync(CreateBookingDto createBookingDto)
        {
            Booking booking = _mapper.Map<Booking>(createBookingDto); 
            await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _bookingRepository.UpdateBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            await _bookingRepository.DeleteBookingAsync(bookingId);
        }
    }
}
