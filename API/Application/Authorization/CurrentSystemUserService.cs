using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Authorization
{
    public class CurrentSystemUserService : ICurrentSystemUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyAuthorizationService _companyAuthorizationService;

        public CurrentSystemUserService(
            IHttpContextAccessor httpContextAccessor,
            ICompanyAuthorizationService companyAuthorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _companyAuthorizationService = companyAuthorizationService;
        }

        public string KeycloakUserId =>
            _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new AuthorizationException("User is not authorized !");

        public async Task<int> GetCompanyIdAsync(CancellationToken cancellationToken)
        {
            return await _companyAuthorizationService
                .GetCurrentUserCompanyIdAsync(KeycloakUserId, cancellationToken);
        }
    }
}
