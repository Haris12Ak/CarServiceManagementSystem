using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientAsync();
        Task<Client> GetClientByIdAsync(int id);
    }
}
