using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class VehicleRepository : EfRepository<Vehicles>, IVehicleRepository
    {
        private readonly IClientRepository _clientRepository;

        public VehicleRepository(
            ApplicationDbContext context,
            IClientRepository clientRepository) : base(context)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<Vehicles>> GetByClientIdAsync(int clientId, int companyId)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .Where(v =>
                    v.ClientId == clientId &&
                    v.CompanyId == companyId &&
                    v.Client.IsActive == true)
                .ToListAsync();
        }

        public override async Task<List<Vehicles>> GetAllAsync(int companyId, bool includeInactive = false)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .Where(v =>
                    v.CompanyId == companyId &&
                    v.Client.IsActive == true)
                .ToListAsync();
        }
    }
}
