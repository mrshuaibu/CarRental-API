using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using CarRental.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetAllCars()
        {
            List<CarDto> cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("by-make")]
        public async Task<ActionResult<List<CarDto>>> GetCarsByMake(string make)
        {
            if (string.IsNullOrEmpty(make))
            {
                return BadRequest("Make is required.");
            }

            List<CarDto> cars = await _carService.GetCarsByMakeAsync(make);

            if (cars == null || cars.Count == 0)
            {
                return NotFound($"No cars found for make: {make}."); 
            }

            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CreateCarDto createCarDto)
        {
            if (createCarDto == null)
            {
                return StatusCode(400, "Car data is required.");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Invalid car data.");
            }

            try
            {
                await _carService.AddCarAsync(createCarDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            if (id != car.CarId)
            {
                return StatusCode(400, "Car ID mismatch.");
            }

            CarDto? existingCar = await _carService.GetCarByIdAsync(id);
            if (existingCar == null)
            {
                return StatusCode(404, "Car not found.");
            }

            await _carService.UpdateCarAsync(car);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            CarDto? car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return StatusCode(404, "Car not found.");
            }

            await _carService.DeleteCarAsync(id);
            return StatusCode(204);
        }
    }
}
