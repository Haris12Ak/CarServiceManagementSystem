using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        Task<Inventory> GetInventoryBySparePartIdAsync(int sparePartId, CancellationToken cancellationToken);
        Task<Inventory> AddStockAsync(int sparePartId, InventoryRequest request, CancellationToken cancellationToken);
        Task<Inventory> RemoveStockAsync(int sparePartId, int workOrderId, InventoryRequest request, CancellationToken cancellationToken);
        Task<Inventory> AdjustInAsync(int sparePartId, InventoryRequest request, CancellationToken cancellationToken);
        Task<Inventory> AdjustOutAsync(int sparePartId, InventoryRequest request, CancellationToken cancellationToken);
        Task<List<InventoryTransactions>> GetInventoryTransactionAsync(int sparePartId, CancellationToken cancellationToken);
    }
}
