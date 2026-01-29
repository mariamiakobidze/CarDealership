using ConsoleApp30.Models;
using System.Collections.Generic;

namespace ConsoleApp30.Services
{
    public interface IEmployeeServices
    {
        void AddEmployee(Employee employee);

        List<Employee> GetAllEmployees();
        Employee? GetEmployeeById(int employeeId);
        List<Employee> SearchEmployeesByName(string name);

        void UpdateEmployee(Employee employee);
        void UpdateEmployeePosition(int employeeId, string newPosition);

        void DeleteEmployee(int employeeId);

        List<Employee> GetEmployeesWithSales();
        int GetEmployeeSalesCount(int employeeId);
        decimal GetEmployeeTotalSalesAmount(int employeeId);
    }
}
