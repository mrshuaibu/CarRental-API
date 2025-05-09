using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL
{
    public class CustomerRepository
    {
        private readonly CarRentalDbContext _context;

        public CustomerRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<List<Customer>> GetCustomersByFullNameAsync(string fullName)
        {
            return await _context.Customers
                                 .Where(c => c.FullName.ToLower().Contains(fullName.ToLower())) 
                                 .ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customer: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            Customer? existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
            if (existingCustomer != null)
            {
                Console.WriteLine($"Updating customer {customer.CustomerId} in database.");

                existingCustomer.FullName = customer.FullName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.DriverLicense = customer.DriverLicense;

                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Customer not found for update.");
            }
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            Customer? customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
