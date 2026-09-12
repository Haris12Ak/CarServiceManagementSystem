using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class VehicleInspections
    {
        [Key]
        public int Id { get; set; }
        public int Mileage { get; set; }
        public DateTime InspectionDate { get; set; }
        public string? GeneralCondition { get; set; }
        public string? Notes { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public Vehicles Vehicles { get; set; }
        public int VehicleId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public WorkOrders WorkOrders { get; set; }
        public int WorkOrderId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public ICollection<InspectionItems> InspectionItems { get; set; } = new List<InspectionItems>();
    }
}
