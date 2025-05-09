using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.BLL
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(CustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            List<Customer> customers = await _customerRepository.GetAllCustomersAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId)
        {
            Customer? customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer); 
        }

        public async Task<List<CustomerDto>> GetCustomersByFullNameAsync(string fullName)
        {
            List<Customer> customers = await _customerRepository.GetCustomersByFullNameAsync(fullName);
            List<CustomerDto> customerDtos = _mapper.Map<List<CustomerDto>>(customers);
            return customerDtos;
        }

        public async Task AddCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            Customer customer = _mapper.Map<Customer>(createCustomerDto); 
            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
        }
    }
}
