using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> FindEmployeeByCompanyIdAsync(int companyId, string keycloakUserId);
        Task<Employee> CreateEmployeeAsync(Employee employee);
    }
}
