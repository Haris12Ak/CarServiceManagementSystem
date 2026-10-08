using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, ICompanyEntity
    {
        Task<List<TEntity>> GetAllAsync(int companyId, CancellationToken cancellationToken, bool includeInactive = false);
        Task<TEntity?> FindByIdAsync(int id, int companyId, CancellationToken cancellationToken, bool includeInactive = false);
        Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, int companyId, CancellationToken cancellationToken);
    }
}
