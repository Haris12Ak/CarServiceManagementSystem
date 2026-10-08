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

        public async Task<Inventory> FindBySparePartIdAsync(int sparePart, int companyId, CancellationToken cancellationToken)
        {
            var inventory = await _context.Inventory
                .AsNoTracking()
                .Include(x => x.SpareParts)
                .Where(x =>
                x.SparePartId == sparePart &&
                x.CompanyId == companyId)
                .FirstOrDefaultAsync(cancellationToken);

            if (inventory == null)
                return null;

            return inventory;
        }

        public Task<List<InventoryTransactions>> GetInventoryTransactionsBySparePartId(int sparePartId, int companyId, CancellationToken cancellationToken)
        {
            var inventoryTransactions = _context.InventoryTransactions
                .AsNoTracking()
                .Where(x =>
                x.SparePartId == sparePartId &&
                x.CompanyId == companyId)
                .ToListAsync(cancellationToken);
            
            return inventoryTransactions;
        }

        public async Task<Inventory> SaveAsync(Inventory inventory, InventoryTransactions inventoryTransactions, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                _context.Inventory.Update(inventory);

                await _context.InventoryTransactions.AddAsync(inventoryTransactions, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

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
