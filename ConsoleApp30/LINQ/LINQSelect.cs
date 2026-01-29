using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp30.LINQ
{
    internal class LINQSelect
    {
        private readonly carDbContext context = new();

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

    }
}
