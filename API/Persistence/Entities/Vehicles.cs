using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class Vehicles
    {
        [Key]
        public int Id { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
        public int ClientId { get; set; }

        public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
        public ICollection<WorkOrders> WorkOrders { get; set; } = new List<WorkOrders>();
        public ICollection<VehicleInspections> VehicleInspections { get; set; } = new List<VehicleInspections>();
        public ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
    }
}
