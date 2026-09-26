using DomainServiceType = Domain.Models.ServiceType;
using EntityServiceType = Persistence.Entities.ServiceType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappers
{
    public static class ServiceTypeMapper
    {
        public static DomainServiceType ToDomain(this EntityServiceType entity)
        {
            return new DomainServiceType
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                EstimatedDuration = entity.EstimatedDuration,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static List<DomainServiceType> ToDomain(this IEnumerable<EntityServiceType> entities)
        {
            return entities.Select(entity => entity.ToDomain()).ToList();
        }

        public static EntityServiceType ToEntity(this DomainServiceType domain)
        {
            return new EntityServiceType
            {
                Id = domain.Id,
                CompanyId = domain.CompanyId,
                Name = domain.Name,
                Description = domain.Description,
                Price = domain.Price,
                EstimatedDuration = domain.EstimatedDuration,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt
            };
        }
    }
}
