using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IClientRepository
    {
        Task<int?> GetCompanyId(string keycloakUserId);
        Task<Client> FindClientByCompanyIdAsync(int companyId, string keycloakUserId);
        Task<Client> CreateClientAsync(Client client);
    }
}
