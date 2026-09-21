using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class Appointments
    {
        [Key]
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public int EstimatedDuration { get; set; }
        public string? Reason { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public Vehicles Vehicles { get; set; }
        public int VehicleId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public ICollection<WorkOrders> WorkOrders { get; set; } = new List<WorkOrders>();
    }
}
