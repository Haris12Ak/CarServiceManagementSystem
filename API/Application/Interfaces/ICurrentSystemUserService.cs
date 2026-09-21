using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICurrentSystemUserService
    {
        string KeycloakUserId { get; }
    }
}
