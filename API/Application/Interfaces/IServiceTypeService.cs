using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceType>> GetAllServiceTypeAsync();
        Task<ServiceType> GetServiceTypeByIdAsync(int id);
        Task<ServiceType> AddServiceTypeAsync(ServiceTypeRequest request);
        Task<ServiceType> UpdateServiceTypeAsync(int id, ServiceTypeRequest request);
    }
}
