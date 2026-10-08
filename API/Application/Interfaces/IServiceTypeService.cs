using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceType>> GetAllServiceTypeAsync(CancellationToken cancellationToken);
        Task<ServiceType> GetServiceTypeByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceType> AddServiceTypeAsync(ServiceTypeRequest request, CancellationToken cancellationToken);
        Task<ServiceType> UpdateServiceTypeAsync(int id, ServiceTypeRequest request, CancellationToken cancellationToken);
    }
}
