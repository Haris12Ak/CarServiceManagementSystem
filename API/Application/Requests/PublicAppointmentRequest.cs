using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class PublicAppointmentRequest
    {
        public DateTime ScheduledAt { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public List<int> ServiceTypeId { get; set; }

        public PublicClientRequest Client { get; set; }
        public PublicVehicleRequest Vehicle { get; set; }
    }

    public class PublicClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? Notes { get; set; }
    }

    public class PublicVehicleRequest
    {
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
