using Application.Requests;
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
        Task<ServiceType> AddAsync(string keycloakUserId, ServiceTypeRequest request);
        Task<ServiceType> UpdateAsync(int id, string keycloakUserId, ServiceTypeRequest request);
    }
}
