using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using CarRental.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings()
        {
            List<BookingDto> bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("by-customer-name")]
        public async Task<ActionResult<List<BookingDto>>> GetBookingsByCustomerName([FromQuery] string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return BadRequest("Customer name is required.");
            }

            List<BookingDto> bookings = await _bookingService.GetBookingsByCustomerNameAsync(fullName);

            if (bookings == null || bookings.Count == 0)
            {
                return NotFound("No bookings found for the specified customer name.");
            }

            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingDto createBookingDto)
        {
            if (createBookingDto == null)
            {
                return StatusCode(400, "Booking data is required.");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Invalid booking data.");
            }

            try
            {
                await _bookingService.AddBookingAsync(createBookingDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return StatusCode(400, "Booking ID mismatch.");
            }

            BookingDto? existingBooking = await _bookingService.GetBookingByIdAsync(id);
            if (existingBooking == null)
            {
                return StatusCode(404, "Booking not found.");
            }

            await _bookingService.UpdateBookingAsync(booking);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            BookingDto? booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return StatusCode(404, "Booking not found.");
            }

            await _bookingService.DeleteBookingAsync(id);
            return StatusCode(204);
        }
    }
}
