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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int BookingIntervalMinutes { get; set; }
        public int MinimumBookingNoticeHours { get; set; }
        public int MaximumBookingDaysAhead { get; set; }

        public bool AllowSameDayBooking { get; set; }
        public bool RequireAppointmentConfirmation { get; set; }
        public bool OnlineBookingEnabled { get; set; }
    }
}
