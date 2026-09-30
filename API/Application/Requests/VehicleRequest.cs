using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class VehicleRequest
    {
        public int ClientId { get; set; }
        public string VIN { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Engine { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public string? Color { get; set; }
        public string? Notes { get; set; }
    }
}
