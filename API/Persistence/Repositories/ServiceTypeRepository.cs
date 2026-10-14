using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Persistence.Repositories
{
    public class ServiceTypeRepository : EfRepository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<ServiceType>> FindByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
        {
            return await _context.ServiceType
                .AsNoTracking()
                .Where(s =>
                s.CompanyId == companyId &&
                s.IsActive == true)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ServiceType>> FindByIdsAsync(IEnumerable<int> ids, int companyId, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any())
                return new List<ServiceType>();

            return await _context.ServiceType
                .AsNoTracking()
                .Where(st => ids.Contains(st.Id) && st.CompanyId == companyId && st.IsActive == true)
                .ToListAsync(cancellationToken);
        }
    }
}
