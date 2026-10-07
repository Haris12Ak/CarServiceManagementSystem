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

        public async Task<bool> IsExistAsync(string partNumber, int companyId)
        {
            var result = partNumber.Trim().ToUpper();

            return await _context.SpareParts
                .AnyAsync(sp => sp.PartNumber == result &&
                sp.CompanyId == companyId &&
                sp.IsActive == true);
        }

        public async Task<SpareParts> SaveAsync(SpareParts entity, int initialQuantity, int employeeId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.SpareParts.AddAsync(entity);

                var inventory = new Inventory
                {
                    SpareParts = entity,
                    CompanyId = entity.CompanyId,
                    Quantity = initialQuantity,
                    UpdatedAt = DateTime.Now
                };

                await _context.Inventory.AddAsync(inventory);

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

                    await _context.InventoryTransactions.AddAsync(inventoryTransaction);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

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
