using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<int?> GetCompanyId(string keycloakUserId);
        Task<Employee> FindByKeycloakId(string keycloakUserId, int companyId);
    }
}
