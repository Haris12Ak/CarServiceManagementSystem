using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicles>
    {
        Task<List<Vehicles>> GetByClientIdAsync(int clientId, int companyId);
    }
}
