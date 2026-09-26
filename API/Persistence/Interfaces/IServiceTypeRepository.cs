using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<List<ServiceType>> FindAllAsync(int companyId);
        Task<ServiceType> FindByIdAsync(int id, int companyId);
    }
}
