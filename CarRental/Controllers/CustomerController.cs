using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using CarRental.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            List<CustomerDto> customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("by-fullname")]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomersByFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return BadRequest("Full name is required.");
            }

            List<CustomerDto> customers = await _customerService.GetCustomersByFullNameAsync(fullName);

            if (customers == null || customers.Count == 0)
            {
                return NotFound("Customer not found.");
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (createCustomerDto == null)
            {
                return StatusCode(400, "Customer data is required.");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Invalid customer data.");
            }

            try
            {
                await _customerService.AddCustomerAsync(createCustomerDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return StatusCode(400, "Customer ID mismatch.");
            }

            CustomerDto? existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                return StatusCode(404, "Customer not found.");
            }

            await _customerService.UpdateCustomerAsync(customer);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            CustomerDto? customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return StatusCode(404, "Customer not found.");
            }

            await _customerService.DeleteCustomerAsync(id);
            return StatusCode(204);
        }
    }
}
