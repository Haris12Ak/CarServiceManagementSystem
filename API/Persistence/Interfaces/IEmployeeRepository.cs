using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<int?> GetCompanyIdAsync(string keycloakUserId);
        Task<Employee> FindByKeycloakIdAsync(string keycloakUserId, int companyId);
    }
}
