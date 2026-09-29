using DomainClient = Domain.Models.Client;
using EntityClient = Persistence.Entities.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappers
{
    public static class ClientMapper
    {
        public static DomainClient ToDomain(this EntityClient entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new DomainClient
            {
                Id = entity.Id,
                KeycloakUserId = entity.KeycloakUserId,
                CompanyId = entity.CompanyId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Address = entity.Address,
                City = entity.City,
                Notes = entity.Notes
            };
        }

        public static List<DomainClient> ToDomain(this IEnumerable<EntityClient> entities)
        {
            return entities.Select(entity => entity.ToDomain()).ToList();
        }

        public static EntityClient ToEntity(this DomainClient domain)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return new EntityClient
            {
                KeycloakUserId = domain.KeycloakUserId,
                CompanyId = domain.CompanyId,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = domain.Email,
                Phone = domain.Phone,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt,
                Address = domain.Address,
                City = domain.City,
                Notes = domain.Notes
            };
        }
    }
}
