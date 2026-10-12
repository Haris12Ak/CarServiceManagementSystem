using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointments>
    {
        Task<bool> HasOverlappingAsync(int companyId, DateTime start, DateTime end, CancellationToken cancellationToken);
        Task SaveAsync(Client client, Vehicles vehicle, Appointments appointment, List<ServiceType> serviceTypes, int employeeId, CancellationToken cancellationToken);
    }
}
