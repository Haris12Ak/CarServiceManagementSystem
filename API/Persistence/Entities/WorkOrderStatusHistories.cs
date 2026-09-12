using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class WorkOrderStatusHistories
    {
        [Key]
        public int Id { get; set; }
        public WorkOrderStatus OldStatus { get; set; }
        public WorkOrderStatus NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
        public string? Notes { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public WorkOrders WorkOrders { get; set; }
        public int WorkOrderId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
