using ConsoleApp30.Models;
using System.Collections.Generic;

namespace ConsoleApp30.Services
{
    public interface IManufacturerServices
    {
        void AddManufacturer(Manufacturer manufacturer);

        List<Manufacturer> GetAllManufacturers();
        Manufacturer? GetManufacturerById(int manufacturerId);
        List<Manufacturer> SearchManufacturersByName(string name);

        void UpdateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(int manufacturerId);

        List<Manufacturer> GetManufacturersWithModels();
        int GetManufacturerModelCount(int manufacturerId);
        int GetManufacturerCarCount(int manufacturerId);
    }
}
