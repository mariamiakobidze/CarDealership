using System;
using ConsoleApp30.Services;
using ConsoleApp30.Models;
using ConsoleApp30.Data;

namespace ConsoleApp30
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var carService = new CarServices();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Car Dealership Management System ===");
                Console.WriteLine("1 - Add new car");
                Console.WriteLine("2 - Show available cars");
                Console.WriteLine("3 - Show all sales with customers and employees");
                Console.WriteLine("4 - Show sold cars with customers");
                Console.WriteLine("5 - Show employee statistics");
                Console.WriteLine("0 - Exit");
                Console.Write("Enter choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("AddCar method selected");
                        var car = new Car();
                        Console.Write("Enter VIN: ");
                        car.Vin = Console.ReadLine();
                        Console.Write("Enter ModelId: ");
                        car.ModelId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Price: ");
                        car.Price = decimal.Parse(Console.ReadLine());
                        carService.AddCar(car);
                        Console.WriteLine("Car added successfully!");
                        break;

                    case "2":
                        Console.WriteLine("Available cars:");
                        var availableCars = carService.GetAvailableCars();
                        foreach (var c in availableCars)
                        {
                            Console.WriteLine($"VIN: {c.Vin}, Model: {c.Model?.Name ?? "No model"}");
                        }
                        break;

                    case "3":
                        Console.WriteLine("All sales with customers and employees:");
                        var sales = carService.GetSalesDetails();
                        foreach (var s in sales)
                            Console.WriteLine(s);
                        break;

                    case "4":
                        Console.WriteLine("Sold cars with customers:");
                        var soldCars = carService.GetSoldCarsWithCustomers();
                        foreach (var s in soldCars)
                            Console.WriteLine(s);
                        break;

                    case "5":
                        Console.WriteLine("Employee statistics:");
                        var stats = carService.GetEmployeeSalesCount();
                        foreach (var s in stats)
                            Console.WriteLine(s);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }
    }
}
