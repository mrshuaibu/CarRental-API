using AutoMapper;
using CarRental.Models;

namespace CarRental.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>(); 

            CreateMap<CreateBookingDto, Booking>(); 
        }
    }
}

