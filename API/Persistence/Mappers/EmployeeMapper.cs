using DomainEmployee = Domain.Models.Employee;
using EntityEmployee = Persistence.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappers
{
    public static class EmployeeMapper
    {
        public static DomainEmployee ToDomain(this EntityEmployee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new DomainEmployee
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
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static EntityEmployee ToEntity(this DomainEmployee domain)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return new EntityEmployee
            {
                KeycloakUserId = domain.KeycloakUserId,
                CompanyId = domain.CompanyId,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = domain.Email,
                Phone = domain.Phone,
                Position = domain.Position,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt
            };
        }
    }
}
