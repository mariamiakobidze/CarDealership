// See https://aka.ms/new-console-template for more information
using ConsoleApp30.Data;
using ConsoleApp30.Services;
using ConsoleApp30.Models;

namespace ConsoleApp30
{
    class Program
    {
        static void Cars(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Car Dealership Management System ===");

            var carService = new CarServices();


            Console.WriteLine("1. CREATE - ახალი მანქანის დამატება:");

            Console.WriteLine("2. READ - ყველა ხელმისაწვდომი მანქანა:");
            var availableCars = carService.GetAvailableCars();
            foreach (var car in availableCars)
            {
                Console.WriteLine($"VIN: {car.Vin}, Model: {car.Model.Name}");
            }
            Console.WriteLine("3. LINQ query 1 - გაყიდული მანქანები:");
            var soldCars = carService.GetSoldCarsWithCustomers();

            Console.WriteLine("4. LINQ query 2 - თანამშრომლების სტატისტიკა:");
            var employeeStats = carService.GetEmployeeSalesCount();
        }
    }
}
