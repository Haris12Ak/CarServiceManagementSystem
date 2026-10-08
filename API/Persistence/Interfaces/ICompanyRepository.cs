using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Companies> FindByIdAsync(int companyId, CancellationToken cancellationToken);
        Task CrateCompanyWithAdminAsync(Companies company, Employee adminEmployee, CancellationToken cancellationToken);
        Task<bool> IsUserInCompanyAsync(string keycloakUserId, int companyId, CancellationToken cancellationToken);
    }
}
