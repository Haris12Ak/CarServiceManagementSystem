using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using DomainInventory = Domain.Models.Inventory;
using DomainInventoryTransactions = Domain.Models.InventoryTransactions;
using EntityInventory = Persistence.Entities.Inventory;
using EntityInventoryTransactions = Persistence.Entities.InventoryTransactions;

namespace Persistence.Mappers
{
    public static class InventoryMapper
    {
        public static DomainInventory ToDomain(this EntityInventory entity)
        {
            return new DomainInventory
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                SparePartId = entity.SparePartId,
                Quantity = entity.Quantity,
                UpdatedAt = entity.UpdatedAt,
                SpareParts = entity.SpareParts.ToDomain()
            };
        }

        public static EntityInventory ToEntity(this DomainInventory domain)
        {
            return new EntityInventory
            {
                CompanyId = domain.CompanyId,
                SparePartId = domain.SparePartId,
                Quantity = domain.Quantity,
                UpdatedAt = domain.UpdatedAt
            };
        }

        public static EntityInventoryTransactions ToEntity(this DomainInventoryTransactions domain)
        {
            return new EntityInventoryTransactions
            {
                Type = domain.Type,
                Quantity = domain.Quantity,
                ReferenceType = domain.ReferenceType,
                ReferenceId = domain.ReferenceId,
                CreatedAt = domain.CreatedAt,
                Note = domain.Note,
                CompanyId = domain.CompanyId,
                SparePartId = domain.SparePartId,
                EmployeeId = domain.EmployeeId
            };
        }

        public static DomainInventoryTransactions ToDomain(this EntityInventoryTransactions entity)
        {
            return new DomainInventoryTransactions
            {
                Id = entity.Id,
                Type = entity.Type,
                Quantity = entity.Quantity,
                ReferenceType = entity.ReferenceType,
                ReferenceId = entity.ReferenceId,
                CreatedAt = entity.CreatedAt,
                Note = entity.Note,
                CompanyId = entity.CompanyId,
                SparePartId = entity.SparePartId,
                EmployeeId = entity.EmployeeId
            };
        }

        public static List<DomainInventoryTransactions> ToDomain(this IEnumerable<EntityInventoryTransactions> entities)
        {
            return entities.Select(entity => entity.ToDomain()).ToList();
        }
    }
}
