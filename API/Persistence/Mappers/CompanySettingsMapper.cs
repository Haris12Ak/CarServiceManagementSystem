using System;
using System.Collections.Generic;
using System.Text;
using DomainCompanySettings = Domain.Models.CompanySettings;
using EntityCompanySettings = Persistence.Entities.CompanySettings;

namespace Persistence.Mappers
{
    public static class CompanySettingsMapper
    {
        public static DomainCompanySettings ToDomain(this EntityCompanySettings entity)
        {
            return new DomainCompanySettings
            {
                Id = entity.Id,
                Currency = entity.Currency,
                TaxRate = entity.TaxRate,
                InvoicePrefix = entity.InvoicePrefix,
                WorkOrderPrefix = entity.WorkOrderPrefix,
                DefaultLaborRate = entity.DefaultLaborRate,

                BookingIntervalMinutes = entity.BookingIntervalMinutes,
                MinimumBookingNoticeHours = entity.MinimumBookingNoticeHours,
                MaximumBookingDaysAhead = entity.MaximumBookingDaysAhead,
                AllowSameDayBooking = entity.AllowSameDayBooking,
                RequireAppointmentConfirmation = entity.RequireAppointmentConfirmation,
                OnlineBookingEnabled = entity.OnlineBookingEnabled,

                CompanyId = entity.CompanyId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
