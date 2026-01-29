using ConsoleApp30.Models;
using System.Collections.Generic;

namespace ConsoleApp30.Services
{
    public interface ISaleServices
    {
        void AddSale(Sale sale);

        List<Sale> GetAllSales();
        Sale? GetSaleById(int saleId);

        void UpdateSaleAmount(int saleId, decimal newAmount);
        void DeleteSale(int saleId);

        List<Sale> GetSalesByCustomer(int customerId);
        List<Sale> GetSalesByEmployee(int employeeId);
        List<Sale> GetSalesByCar(int carId);

        decimal GetTotalRevenue();
        int GetTotalSalesCount();
    }
}