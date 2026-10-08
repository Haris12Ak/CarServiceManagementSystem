using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Task<Inventory> FindBySparePartIdAsync(int sparePart, int companyId, CancellationToken cancellationToken);
        Task<Inventory> SaveAsync(Inventory inventory, InventoryTransactions inventoryTransactions, CancellationToken cancellationToken);
        Task<List<InventoryTransactions>> GetInventoryTransactionsBySparePartId(int sparePartId, int companyId, CancellationToken cancellationToken);
    }
}
