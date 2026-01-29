using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp30.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private carDbContext context = new carDbContext();

        public void AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public List<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            return context.Employees
                .Include(e => e.Sales)
                .SingleOrDefault(e => e.EmployeeId == employeeId);
        }

        public List<Employee> SearchEmployeesByName(string name)
        {
            return context.Employees
                .Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name))
                .ToList();
        }

        public void UpdateEmployee(Employee employee)
        {
            var existing = context.Employees.Find(employee.EmployeeId);
            if (existing == null) throw new ArgumentException("Employee not found");

            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;;
            existing.Position = employee.Position;

            context.SaveChanges();
        }

        public void UpdateEmployeePosition(int employeeId, string newPosition)
        {
            var employee = context.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.Position = newPosition;
                context.SaveChanges();
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }

        public List<Employee> GetEmployeesWithSales()
        {
            return context.Employees
                .Include(e => e.Sales)
                .Where(e => e.Sales.Any())
                .ToList();
        }

        public int GetEmployeeSalesCount(int employeeId)
        {
            return context.Sales
                .Where(s => s.EmployeeId == employeeId)
                .Count();
        }

        public decimal GetEmployeeTotalSalesAmount(int employeeId)
        {
            return context.Sales
                .Where(s => s.EmployeeId == employeeId)
                .Sum(s => s.TotalAmount);
        }
    }
}
