using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authorization
{
    public interface ICurrentSystemUserService
    {
        string KeycloakUserId { get; }
    }
}
