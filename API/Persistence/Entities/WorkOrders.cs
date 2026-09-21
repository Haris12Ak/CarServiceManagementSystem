using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class WorkOrders
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string? Description { get; set; }
        public int Mileage { get; set; }
        public WorkOrderStatus Status { get; set; }
        public string? Priority { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
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

        [ForeignKey(nameof(AppointmentId))]
        public Appointments Appointments { get; set; }
        public int AppointmentId { get; set; }

        public ICollection<WorkOrderStatusHistories> WorkOrderStatusHistories { get; set; } = new List<WorkOrderStatusHistories>();
        public ICollection<WorkOrderServices> WorkOrderServices { get; set; } = new List<WorkOrderServices>();
        public ICollection<WorkOrderParts> WorkOrderParts { get; set; } = new List<WorkOrderParts>();
        public ICollection<VehicleInspections> VehicleInspections { get; set; } = new List<VehicleInspections>();
        public ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
    }

}
