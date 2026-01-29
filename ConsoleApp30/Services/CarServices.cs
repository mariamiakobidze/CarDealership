using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Car> GetAllCars()
        {
            return context.Cars
                .Include(c => c.Model)
                .ThenInclude(m => m.Manufacturer)
                .ToList();
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

        public List<string> GetSalesDetails()
        {
            return context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Manufacturer)
                .Include(s => s.Employee)
                .Select(s => $"SaleId: {s.SaleId}, Customer: {s.Customer.FirstName} {s.Customer.LastName}, Car: {s.Car.Vin} ({s.Car.Model.Name}), Manufacturer: {s.Car.Model.Manufacturer.Name}, Employee: {s.Employee.FirstName} {s.Employee.LastName}, TotalAmount: {s.TotalAmount}")
                .ToList();
        }

        public List<string> GetSoldCarsWithCustomers()
        {
            return context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Manufacturer)
                .Where(s => s.Car.Status == "Sold")
                .Select(s => $"SaleId: {s.SaleId}, Customer: {s.Customer.FirstName} {s.Customer.LastName}, Car: {s.Car.Vin} ({s.Car.Model.Name}), Manufacturer: {s.Car.Model.Manufacturer.Name}, TotalAmount: {s.TotalAmount}")
                .ToList();
        }

        public List<string> GetEmployeeSalesCount()
        {
            return context.Sales
                .Include(s => s.Employee)
                .GroupBy(s => new { s.Employee.EmployeeId, s.Employee.FirstName, s.Employee.LastName })
                .Select(g => $"Employee: {g.Key.FirstName} {g.Key.LastName}, SalesCount: {g.Count()}, TotalSales: {g.Sum(s => s.TotalAmount)}")
                .ToList();
        }

        public void UpdateCar(Car car)
        {
            var existingCar = context.Cars.SingleOrDefault(c => c.CarId == car.CarId);
            if (existingCar == null)
                throw new ArgumentException("Car not found");

            existingCar.Price = car.Price;
            existingCar.Year = car.Year;
            existingCar.Status = car.Status;
            existingCar.ModelId = car.ModelId;

            context.SaveChanges();
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
    }
}
