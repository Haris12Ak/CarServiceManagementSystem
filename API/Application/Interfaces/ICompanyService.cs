using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task RegisterCompanyAsync(CompanyRegistration registration, CancellationToken cancellationToken);
        Task AddEmployeeToCompanyAsync(EmployeeInsertRequest request, CancellationToken cancellationToken);
        Task AddClientToCompanyAsync(ClientInsertRequest request, CancellationToken cancellationToken);
    }
}
