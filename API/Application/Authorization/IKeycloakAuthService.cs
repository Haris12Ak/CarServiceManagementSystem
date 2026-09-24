using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authorization
{
    public interface IKeycloakAuthService
    {
        Task AuthenticateAdminAsync();
        Task<string> CreateUserAsync(User user, bool forcePasswordUpdated);
        Task DeleteUserAsync(string keycloakUserId);
        Task AssignRoleAsync(string keycloakUserId, string roleName);
    }
}
