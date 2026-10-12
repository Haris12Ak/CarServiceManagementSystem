using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IServiceTypeRepository : IRepository<ServiceType> 
    {
        Task<List<ServiceType>> FindByIdsAsync(IEnumerable<int> ids, int companyId, CancellationToken cancellationToken);
    }
}
