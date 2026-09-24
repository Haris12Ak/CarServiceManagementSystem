using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Application.Authorization
{
    public interface ICompanyAuthorizationService
    {
        Task<int> GetCurrentUserCompanyIdAsync(string keycloakUserId);
        Task<bool> IsUserInCompanyAsync(string keycloakUserId, int companyId);
    }
}
