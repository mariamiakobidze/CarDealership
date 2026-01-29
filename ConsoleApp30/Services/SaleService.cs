using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp30.Services
{
    public class SaleServices : ISaleServices
    {
        private carDbContext context = new carDbContext();

        public void AddSale(Sale sale)
        {
            var car = context.Cars.Find(sale.CarId);
            if (car == null) throw new ArgumentException("Car not found");
            if (car.Status == "Sold") throw new InvalidOperationException("Car already sold");

            car.Status = "Sold";
            context.Sales.Add(sale);
            context.SaveChanges();
        }

        public List<Sale> GetAllSales()
        {
            return context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .Include(s => s.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Manufacturer)
                .ToList();
        }

        public Sale? GetSaleById(int saleId)
        {
            return context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .Include(s => s.Car)
                .SingleOrDefault(s => s.SaleId == saleId);
        }

        public void UpdateSaleAmount(int saleId, decimal newAmount)
        {
            var sale = context.Sales.Find(saleId);
            if (sale != null)
            {
                sale.TotalAmount = newAmount;
                context.SaveChanges();
            }
        }

        public void DeleteSale(int saleId)
        {
            var sale = context.Sales.Find(saleId);
            if (sale != null)
            {
                var car = context.Cars.Find(sale.CarId);
                if (car != null) car.Status = "Available";

                context.Sales.Remove(sale);
                context.SaveChanges();
            }
        }

        public List<Sale> GetSalesByCustomer(int customerId)
        {
            return context.Sales
                .Where(s => s.CustomerId == customerId)
                .Include(s => s.Car)
                .ToList();
        }

        public List<Sale> GetSalesByEmployee(int employeeId)
        {
            return context.Sales
                .Where(s => s.EmployeeId == employeeId)
                .Include(s => s.Car)
                .ToList();
        }

        public List<Sale> GetSalesByCar(int carId)
        {
            return context.Sales
                .Where(s => s.CarId == carId)
                .ToList();
        }

        public decimal GetTotalRevenue()
        {
            return context.Sales.Sum(s => s.TotalAmount);
        }

        public int GetTotalSalesCount()
        {
            return context.Sales.Count();
        }
    }
}
