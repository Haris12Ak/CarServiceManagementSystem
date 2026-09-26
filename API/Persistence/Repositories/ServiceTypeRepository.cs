using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Persistence.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceType>> FindAllAsync(int companyId)
        {
            return await _context.ServiceType.
                Where(x => x.CompanyId == companyId && x.IsActive == true)
                .ToListAsync();
        }

        public async Task<ServiceType> FindByIdAsync(int id, int companyId)
        {
            var serviceType = await _context.ServiceType
                .Where(x =>
                x.Id == id &&
                x.CompanyId == companyId &&
                x.IsActive == true)
                .FirstOrDefaultAsync();

            if (serviceType == null)
                return null;

            return serviceType;
        }
    }
}
