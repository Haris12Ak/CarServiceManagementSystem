using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface ISparePartsRepository : IRepository<SpareParts>
    {
        Task<bool> IsExistAsync(string partNumber, int companyId, CancellationToken cancellationToken);
        Task<SpareParts> SaveAsync(SpareParts entity, int initialQuantity, int employeeId, CancellationToken cancellationToken);
    }
}
