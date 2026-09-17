using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Defaults
{
    public static class DefaultCompanySettings
    {
        public const string Currency = "BAM";
        public const decimal TaxRate = 17m;
        public const string InvoicePrefix = "INV";
        public const string WorkOrderPrefix = "WO";
        public const decimal DefaultLaborRate = 0m;
        public const int AppointmentDuration = 30;

        public static CompanySettings Create() => new CompanySettings
        {
            Currency = Currency,
            TaxRate = TaxRate,
            InvoicePrefix = InvoicePrefix,
            WorkOrderPrefix = WorkOrderPrefix,
            DefaultLaborRate = DefaultLaborRate,
            AppointmentDuration = AppointmentDuration,
            CreatedAt = DateTime.Now
        };
    }
}
