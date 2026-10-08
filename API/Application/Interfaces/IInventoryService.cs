using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        Task<Inventory> GetInventoryBySparePartIdAsync(int sparePartId);
        Task<Inventory> AddStockAsync(int sparePartId, InventoryRequest request);
        Task<Inventory> RemoveStockAsync(int sparePartId, int workOrderId, InventoryRequest request);
        Task<Inventory> AdjustInAsync(int sparePartId, InventoryRequest request);
        Task<Inventory> AdjustOutAsync(int sparePartId, InventoryRequest request);
        Task<List<InventoryTransactions>> GetInventoryTransactionAsync(int sparePartId);
    }
}
