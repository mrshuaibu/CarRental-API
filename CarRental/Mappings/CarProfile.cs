using AutoMapper;
using CarRental.Models;

namespace CarRental.Mappings
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();

            CreateMap<CreateCarDto, Car>();
        }
    }
}

