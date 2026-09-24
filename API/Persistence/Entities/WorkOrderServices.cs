using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class WorkOrderServices
    {
        [Key]
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public WorkOrders WorkOrders { get; set; }
        public int WorkOrderId { get; set; }

        [ForeignKey(nameof(ServiceTypeId))]
        public ServiceType ServiceType { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
