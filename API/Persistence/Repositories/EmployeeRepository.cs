using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> GetCompanyId(string keycloakUserId)
        {
            var companyId = await _context.Employee
                .Where(x => x.KeycloakUserId == keycloakUserId && x.IsActive == true)
                .Select(x => (int?)x.CompanyId)
                .FirstOrDefaultAsync();

            return companyId;
        }

        public async Task<Employee> FindEmployeeByCompanyIdAsync(int companyId, string keycloakUserId)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.KeycloakUserId == keycloakUserId &&
                x.IsActive == true);

            if (employee == null)
                return null;

            return employee;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}
