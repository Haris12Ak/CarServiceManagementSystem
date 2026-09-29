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

        public async Task<int?> GetCompanyId(string keycloakUserId)
        {
            var companyId = await _context.Employee
                .Where(x => x.KeycloakUserId == keycloakUserId && x.IsActive == true)
                .Select(x => (int?)x.CompanyId)
                .FirstOrDefaultAsync();

            return companyId;
        }
    }
}
