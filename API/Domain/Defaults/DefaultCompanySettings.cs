using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Defaults
{
    public static class DefaultCompanySettings
    {
        public const string currency = "BAM";
        public const decimal taxRate = 17m;
        public const string invoicePrefix = "INV";
        public const string workOrderPrefix = "WO";
        public const decimal defaultLaborRate = 0m;
        public const int bookingIntervalMinutes = 30;
        public const int minimumBookingNoticeHours = 2;
        public const int maximumBookingDaysAhead = 30;
        public const bool allowSameDayBooking = true;
        public const bool requireAppointmentConfirmation = true;
        public const bool onlineBookingEnabled = false;

        public static CompanySettings Create() => new CompanySettings
        {
            Currency = currency,
            TaxRate = taxRate,
            InvoicePrefix = invoicePrefix,
            WorkOrderPrefix = workOrderPrefix,
            DefaultLaborRate = defaultLaborRate,
            CreatedAt = DateTime.Now,
            BookingIntervalMinutes = bookingIntervalMinutes,
            MinimumBookingNoticeHours = minimumBookingNoticeHours,
            MaximumBookingDaysAhead = maximumBookingDaysAhead,
            AllowSameDayBooking = allowSameDayBooking,
            RequireAppointmentConfirmation = requireAppointmentConfirmation,
            OnlineBookingEnabled = onlineBookingEnabled
        };
    }
}
