using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task RegisterCompanyAsync(CompanyRegistration registration);
        Task AddEmployeeToCompanyAsync(int companyId, string keycloakUserId, EmployeeInsertRequest request);
        Task AddClientToCompanyAsync(int companyId, string keycloakUserId, ClientInsertRequest request);
    }
}
