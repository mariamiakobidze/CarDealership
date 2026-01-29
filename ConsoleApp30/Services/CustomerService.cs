using ConsoleApp30.Models;
using System.Collections.Generic;

namespace ConsoleApp30.Services
{
   
    public interface ICustomerServices
    {
   
        void AddCustomer(Customer customer);

   
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(int customerId);
        Customer? GetCustomerByPhone(string phone);
        List<Customer> SearchCustomersByName(string name);

   
        void UpdateCustomer(Customer customer);
        void UpdateCustomerPhone(int customerId, string newPhone);

   
        void DeleteCustomer(int customerId);

   
        List<Customer> GetCustomersWithSales();
        List<Customer> GetCustomersWithTestDrives();
        int GetCustomerTotalPurchases(int customerId);
    }
}