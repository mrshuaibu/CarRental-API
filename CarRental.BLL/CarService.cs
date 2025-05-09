using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.BLL
{
    public class CarService
    {
        private readonly CarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(CarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<List<CarDto>> GetAllCarsAsync()
        {
            List<Car> cars = await _carRepository.GetAllCarsAsync();
            return _mapper.Map<List<CarDto>>(cars);
        }

        public async Task<CarDto?> GetCarByIdAsync(int carId)
        {
            Car? car = await _carRepository.GetCarByIdAsync(carId);
            return car == null ? null : _mapper.Map<CarDto>(car);
        }

        public async Task<List<CarDto>> GetCarsByMakeAsync(string make)
        {
            List<Car> cars = await _carRepository.GetCarsByMakeAsync(make); 

            List<CarDto> carDtos = _mapper.Map<List<CarDto>>(cars);
            return carDtos;
        }

        public async Task AddCarAsync(CreateCarDto createCarDto)
        {
            Car car = _mapper.Map<Car>(createCarDto); 
            await _carRepository.AddCarAsync(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            await _carRepository.UpdateCarAsync(car);
        }

        public async Task DeleteCarAsync(int carId)
        {
            await _carRepository.DeleteCarAsync(carId);
        }
    }
}
