using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<int?> GetCompanyIdAsync(string keycloakUserId, CancellationToken cancellationToken);
        Task<Employee> FindByKeycloakIdAsync(string keycloakUserId, int companyId, CancellationToken cancellationToken);
        Task<int?> FindAvailableEmployeeAsync(int companyId, DateTime start, DateTime end, CancellationToken cancellationToken, int? employeeId = null);
    }
}
