using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using DomainSpareParts = Domain.Models.SpareParts;
using EntitySpareParts = Persistence.Entities.SpareParts;

namespace Persistence.Mappers
{
    public static class SparePartsMapper
    {
        public static EntitySpareParts ToEntity(this DomainSpareParts domain)
        {
            return new EntitySpareParts
            {
                CompanyId = domain.CompanyId,
                SupplierId = domain.SupplierId,
                PartNumber = domain.PartNumber,
                Name = domain.Name,
                Description = domain.Description,
                PurchasePrice = domain.PurchasePrice,
                SellingPrice = domain.SellingPrice,
                MinimumStock = domain.MinimumStock,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt
            };
        }

        public static DomainSpareParts ToDomain(this EntitySpareParts entity)
        {
            return new DomainSpareParts
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                SupplierId = entity.SupplierId,
                PartNumber = entity.PartNumber,
                Name = entity.Name,
                Description = entity.Description,
                PurchasePrice = entity.PurchasePrice,
                SellingPrice = entity.SellingPrice,
                MinimumStock = entity.MinimumStock,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static List<DomainSpareParts> ToDomain(this IEnumerable<EntitySpareParts> entities)
        {
            return entities.Select(entity => entity.ToDomain()).ToList();
        }

        public static void ApplyTo(this DomainSpareParts domain, EntitySpareParts entity)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.SupplierId = domain.SupplierId;
            entity.PartNumber = domain.PartNumber;
            entity.Name = domain.Name;
            entity.Description = domain.Description;
            entity.PurchasePrice = domain.PurchasePrice;
            entity.SellingPrice = domain.SellingPrice;
            entity.MinimumStock = domain.MinimumStock;
            entity.UpdatedAt = DateTime.Now;
        }
    }
}
