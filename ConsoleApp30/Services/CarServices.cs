using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp30.Services
{
    public class CarServices : ICarServices
    {
        private carDbContext context = new carDbContext();
        public void AddCar(Car car)
        {
            context.Cars.Add(car);
            context.SaveChanges();
        }
        public List<Car> GetAllCars()
        {
            return context.Cars
                .Include(c => c.Model)
                .ThenInclude(m => m.Manufacturer)
                .ToList();
        }

        public void UpdateCarStatus(int carId, string status)
        {
            var car = context.Cars.Find(carId);
            if (car != null)
            {
                car.Status = status;
                context.SaveChanges();
            }
        }


        public void DeleteCar(int carId)
        {
            var car = context.Cars.Find(carId);
            if (car != null)
            {
                context.Cars.Remove(car);
                context.SaveChanges();
            }
        }

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void AddSale(Sale sale)
        {
            var car = context.Cars.Find(sale.CarId);

            if (car != null && car.Status == "Available")

                car.Status = "Sold";

            context.Sales.Add(sale);
            context.SaveChanges();
        }

        public Car GetCarById(int id)
        {
            var car = context.Cars
                .Include(c => c.Model)
                .ThenInclude(m => m.Manufacturer)
                .SingleOrDefault(c => c.CarId == id);

            if (car == null)
                throw new ArgumentException("Car not found");

            return car;
        }



        public List<Car> GetAvailableCars()
        {
            return context.Cars
                .Include(c => c.Model)
                .ThenInclude(m => m.Manufacturer)
                .Where(c => c.Status == "Available")
                .ToList();
        }


        public void UpdateCar(Car car)
        {
            var existingCar = context.Cars
                .SingleOrDefault(c => c.CarId == car.CarId);

            if (existingCar == null)
                throw new ArgumentException("Car not found");

            existingCar.Price = car.Price;
            existingCar.Year = car.Year;
            existingCar.Status = car.Status;
            existingCar.ModelId = car.ModelId;

            context.SaveChanges();
        }

    }
}
