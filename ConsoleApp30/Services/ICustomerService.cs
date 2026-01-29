using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp30.Services
{
    public class CustomerServices : ICustomerServices
    {
        private carDbContext context = new carDbContext();

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer? GetCustomerById(int customerId)
        {
            return context.Customers
                .Include(c => c.Sales)
                .Include(c => c.TestDrives)
                .SingleOrDefault(c => c.CustomerId == customerId);
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            return context.Customers
                .SingleOrDefault(c => c.Phone == phone);
        }

        public List<Customer> SearchCustomersByName(string name)
        {
            return context.Customers
                .Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name))
                .ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existing = context.Customers.Find(customer.CustomerId);
            if (existing == null) throw new ArgumentException("Customer not found");

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Phone = customer.Phone;

            context.SaveChanges();
        }

        public void UpdateCustomerPhone(int customerId, string newPhone)
        {
            var customer = context.Customers.Find(customerId);
            if (customer != null)
            {
                customer.Phone = newPhone;
                context.SaveChanges();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = context.Customers.Find(customerId);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }

        public List<Customer> GetCustomersWithSales()
        {
            return context.Customers
                .Include(c => c.Sales)
                .Where(c => c.Sales.Any())
                .ToList();
        }

        public List<Customer> GetCustomersWithTestDrives()
        {
            return context.Customers
                .Include(c => c.TestDrives)
                .Where(c => c.TestDrives.Any())
                .ToList();
        }

        public int GetCustomerTotalPurchases(int customerId)
        {
            return context.Sales
                .Where(s => s.CustomerId == customerId)
                .Count();
        }
    }
}
