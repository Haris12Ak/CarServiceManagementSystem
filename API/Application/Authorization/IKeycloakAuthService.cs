using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authorization
{
    public interface IKeycloakAuthService
    {
        Task AuthenticateAdminAsync(CancellationToken cancellationToken);
        Task<string> CreateUserAsync(User user, bool forcePasswordUpdated, CancellationToken cancellationToken);
        Task DeleteUserAsync(string keycloakUserId, CancellationToken cancellationToken);
        Task AssignRoleAsync(string keycloakUserId, string roleName, CancellationToken cancellationToken);
    }
}
