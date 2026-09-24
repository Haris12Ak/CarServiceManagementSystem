using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task RegisterCompanyAsync(CompanyRegistration registration);
        Task AddEmployeeToCompanyAsync(string keycloakUserId, EmployeeInsertRequest request);
        Task AddClientToCompanyAsync(string keycloakUserId, ClientInsertRequest request);
    }
}
