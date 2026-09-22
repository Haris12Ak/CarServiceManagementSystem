using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> CreateClientAsync(Client client);
    }
}
