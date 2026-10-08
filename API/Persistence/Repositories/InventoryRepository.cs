using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class InventoryRepository : EfRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Inventory> FindBySparePartIdAsync(int sparePart, int companyId)
        {
            var inventory = await _context.Inventory
                .AsNoTracking()
                .Where(x =>
                x.SparePartId == sparePart &&
                x.CompanyId == companyId)
                .FirstOrDefaultAsync();

            if (inventory == null)
                return null;

            return inventory;
        }

        public Task<List<InventoryTransactions>> GetInventoryTransactionsBySparePartId(int sparePartId, int companyId)
        {
            var inventoryTransactions = _context.InventoryTransactions
                .AsNoTracking()
                .Where(x =>
                x.SparePartId == sparePartId &&
                x.CompanyId == companyId)
                .ToListAsync();

            return inventoryTransactions;
        }

        public async Task<Inventory> SaveAsync(Inventory inventory, InventoryTransactions inventoryTransactions)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Inventory.Update(inventory);

                await _context.InventoryTransactions.AddAsync(inventoryTransactions);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return inventory;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
