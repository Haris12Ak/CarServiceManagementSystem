using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientAsync(CancellationToken cancellationToken);
        Task<Client> GetClientByIdAsync(int id, CancellationToken cancellationToken);
    }
}
