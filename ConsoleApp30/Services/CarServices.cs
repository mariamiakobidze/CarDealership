using ConsoleApp30.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp30.Services
{
    internal class CarServices : ICarServices

    {
        private carDbContext context = new carDbContext();
        public CarServices() { }

    }
}
