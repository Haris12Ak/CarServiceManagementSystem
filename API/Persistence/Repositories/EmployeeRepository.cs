using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class EmployeeRepository : EfRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<int?> GetCompanyIdAsync(string keycloakUserId, CancellationToken cancellationToken)
        {
            var companyId = await _context.Employee
                .Where(x => x.KeycloakUserId == keycloakUserId && x.IsActive == true)
                .Select(x => (int?)x.CompanyId)
                .FirstOrDefaultAsync(cancellationToken);

            return companyId;
        }

        public async Task<Employee> FindByKeycloakIdAsync(string keycloakUserId, int companyId, CancellationToken cancellationToken)
        {
            var employee = await _context.Employee
                .AsNoTracking()
                .Where(x =>
                   x.KeycloakUserId == keycloakUserId &&
                   x.CompanyId == companyId &&
                   x.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);

            if (employee == null)
                return null;

            return employee;
        }
    }
}
