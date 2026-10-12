using System;
using System.Collections.Generic;
using System.Text;
using DomainAppointments = Domain.Models.Appointments;
using EntityAppointments = Persistence.Entities.Appointments;

namespace Persistence.Mappers
{
    public static class AppointmentMapper
    {
        public static EntityAppointments ToEntity(this DomainAppointments domain, int estimatedDuration)
        {
            return new EntityAppointments
            {
                ClientId = domain.ClientId,
                VehicleId = domain.VehicleId,
                CompanyId = domain.CompanyId,
                EmployeeId = domain.EmployeeId,
                ScheduledAt = domain.ScheduledAt,
                EstimatedDuration = estimatedDuration,
                Reason = domain.Reason,
                Status = domain.Status,
                Notes = domain.Notes,
                CreatedAt = domain.CreatedAt
            };
        }
    }
}
