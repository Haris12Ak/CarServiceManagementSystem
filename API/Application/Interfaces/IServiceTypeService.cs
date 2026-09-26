using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<ServiceType> GetByIdAsync(int id, string keycloakUserId);
        Task<List<ServiceType>> GetAllAsync(string keycloakUserId);
    }
}
