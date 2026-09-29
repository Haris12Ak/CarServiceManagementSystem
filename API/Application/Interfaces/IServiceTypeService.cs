using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceType>> GetAllServiceTypeAsync(string keycloakUserId);
        Task<ServiceType> GetServiceTypeByIdAsync(int id, string keycloakUserId);
        Task<ServiceType> AddServiceTypeAsync(string keycloakUserId, ServiceTypeRequest request);
        Task<ServiceType> UpdateServiceTypeAsync(int id, string keycloakUserId, ServiceTypeRequest request);
    }
}
