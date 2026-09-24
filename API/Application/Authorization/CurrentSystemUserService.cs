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

        public CurrentSystemUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string KeycloakUserId =>
            _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("User is not authorized !");
    }
}
