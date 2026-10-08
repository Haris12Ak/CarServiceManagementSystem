using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Persistence.Repositories
{
    public class SparePartsRepository : EfRepository<SpareParts>, ISparePartsRepository
    {
        public SparePartsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> IsExistAsync(string partNumber, int companyId, CancellationToken cancellationToken)
        {
            var result = partNumber.Trim().ToUpper();

            return await _context.SpareParts
                .AnyAsync(sp => sp.PartNumber == result &&
                sp.CompanyId == companyId &&
                sp.IsActive == true,
                cancellationToken);
        }

        public async Task<SpareParts> SaveAsync(SpareParts entity, int initialQuantity, int employeeId, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _context.SpareParts.AddAsync(entity, cancellationToken);

                var inventory = new Inventory
                {
                    SpareParts = entity,
                    CompanyId = entity.CompanyId,
                    Quantity = initialQuantity,
                    UpdatedAt = DateTime.Now
                };

                await _context.Inventory.AddAsync(inventory, cancellationToken);

                if (initialQuantity > 0)
                {
                    var inventoryTransaction = new InventoryTransactions
                    {
                        CompanyId = entity.CompanyId,
                        SpareParts = entity,
                        EmployeeId = employeeId,
                        Type = InventoryTransactionType.In,
                        Quantity = initialQuantity,
                        ReferenceType = InventoryTransactionReferenceType.InitialStock,
                        CreatedAt = DateTime.Now,
                    };

                    await _context.InventoryTransactions.AddAsync(inventoryTransaction, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return entity;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
