using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class CompanySettings
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Currency { get; set; }
        public decimal TaxRate { get; set; }
        public string InvoicePrefix { get; set; }
        public string WorkOrderPrefix { get; set; }
        public decimal DefaultLaborRate { get; set; }
        public int AppointmentDuration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
