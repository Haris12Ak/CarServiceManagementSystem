using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class AppointmentRequest
    {
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public List<int> ServiceTypeId { get; set; }
    }
}
