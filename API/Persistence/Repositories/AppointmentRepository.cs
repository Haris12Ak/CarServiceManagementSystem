using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class AppointmentRepository : EfRepository<Appointments>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> HasOverlappingAsync(int companyId, DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .AsNoTracking()
                .AnyAsync(a =>
                    a.CompanyId == companyId &&
                    a.ScheduledAt < end &&
                    a.ScheduledAt.AddMinutes(a.EstimatedDuration) > start,
                    cancellationToken);
        }

        public async Task SaveAsync(Client client, Vehicles vehicle, Appointments appointment, List<ServiceType> serviceTypes, int employeeId, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _context.Client.AddAsync(client, cancellationToken);

                vehicle.Client = client;
                await _context.Vehicles.AddAsync(vehicle, cancellationToken);

                appointment.Client = client;
                appointment.Vehicles = vehicle;
                appointment.EmployeeId = employeeId;
                await _context.Appointments.AddAsync(appointment, cancellationToken);

                foreach (var st in serviceTypes)
                {
                    appointment.AppointmentServices.Add(new AppointmentServices
                    {
                        Appointments = appointment,
                        ServiceTypeId = st.Id,
                        Price = st.Price,
                        EstimatedDuration = st.EstimatedDuration,
                        CompanyId = appointment.CompanyId
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }

        public async Task SaveAsync(Appointments appointment, List<ServiceType> serviceTypes, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _context.Appointments.AddAsync(appointment);

                foreach (var st in serviceTypes)
                {
                    appointment.AppointmentServices.Add(new AppointmentServices
                    {
                        Appointments = appointment,
                        ServiceTypeId = st.Id,
                        Price = st.Price,
                        EstimatedDuration = st.EstimatedDuration,
                        CompanyId = appointment.CompanyId
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
