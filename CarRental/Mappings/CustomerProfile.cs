using AutoMapper;
using CarRental.Models;

namespace CarRental.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CreateCustomerDto, Customer>(); 
        }
    }
}
