using ConsoleApp30.Models;
using System.Collections.Generic;

namespace ConsoleApp30.Services
{
    public interface ICarServices
    {
        void AddCar(Car car);
        void AddCustomer(Customer customer);
        void AddSale(Sale sale);

        List<Car> GetAllCars();
        Car GetCarById(int id);
        List<Car> GetAvailableCars();

        void UpdateCar(Car car);
        void UpdateCarStatus(int carId, string status);
        void DeleteCar(int carId);

        List<string> GetSalesDetails();
        List<string> GetSoldCarsWithCustomers();
        List<string> GetEmployeeSalesCount();
    }
}
