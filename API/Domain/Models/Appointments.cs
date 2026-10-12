using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public int EstimatedDuration { get; set; }
        public string? Reason { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
