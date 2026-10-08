using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Task<Inventory> FindBySparePartIdAsync(int sparePart, int companyId);
        Task<Inventory> SaveAsync(Inventory inventory, InventoryTransactions inventoryTransactions);
        Task<List<InventoryTransactions>> GetInventoryTransactionsBySparePartId(int sparePartId, int companyId);
    }
}
