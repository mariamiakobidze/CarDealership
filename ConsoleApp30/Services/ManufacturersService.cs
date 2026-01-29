using ConsoleApp30.Data;
using ConsoleApp30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp30.Services
{
    public class ManufacturerServices : IManufacturerServices
    {
        private carDbContext context = new carDbContext();

        public void AddManufacturer(Manufacturer manufacturer)
        {
            context.Manufacturers.Add(manufacturer);
            context.SaveChanges();
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return context.Manufacturers.ToList();
        }

        public Manufacturer? GetManufacturerById(int manufacturerId)
        {
            return context.Manufacturers
                .Include(m => m.Models)
                .ThenInclude(model => model.Cars)
                .SingleOrDefault(m => m.ManufacturerId == manufacturerId);
        }

        public List<Manufacturer> SearchManufacturersByName(string name)
        {
            return context.Manufacturers
                .Where(m => m.Name.Contains(name))
                .ToList();
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            var existing = context.Manufacturers.Find(manufacturer.ManufacturerId);
            if (existing == null) throw new ArgumentException("Manufacturer not found");

            existing.Name = manufacturer.Name;
            existing.Country = manufacturer.Country;

            context.SaveChanges();
        }

        public void DeleteManufacturer(int manufacturerId)
        {
            var manufacturer = context.Manufacturers.Find(manufacturerId);
            if (manufacturer != null)
            {
                context.Manufacturers.Remove(manufacturer);
                context.SaveChanges();
            }
        }

        public List<Manufacturer> GetManufacturersWithModels()
        {
            return context.Manufacturers
                .Include(m => m.Models)
                .Where(m => m.Models.Any())
                .ToList();
        }

        public int GetManufacturerModelCount(int manufacturerId)
        {
            return context.Models
                .Where(m => m.ManufacturerId == manufacturerId)
                .Count();
        }

        public int GetManufacturerCarCount(int manufacturerId)
        {
            return context.Cars
                .Include(c => c.Model)
                .Where(c => c.Model.ManufacturerId == manufacturerId)
                .Count();
        }
    }
}
