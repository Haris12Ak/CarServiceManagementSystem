using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DomainSuppliers = Domain.Models.Suppliers;
using EntitySuppliers = Persistence.Entities.Suppliers;

namespace Persistence.Mappers
{
    public static class SupplierMapper
    {
        public static DomainSuppliers ToDomain(this EntitySuppliers entity)
        {
            return new DomainSuppliers
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Name = entity.Name,
                ContactPerson = entity.ContactPerson,
                Email = entity.Email,
                Phone = entity.Phone,
                Address = entity.Address,
                City = entity.City,
                TaxNumber = entity.TaxNumber,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static List<DomainSuppliers> ToDomain(this IEnumerable<EntitySuppliers> entities)
        {
            return entities.Select(entity => entity.ToDomain()).ToList();
        }

        public static EntitySuppliers ToEntity(this DomainSuppliers domain)
        {
            return new EntitySuppliers
            {
                CompanyId = domain.CompanyId,
                Name = domain.Name,
                ContactPerson = domain.ContactPerson,
                Email = domain.Email,
                Phone = domain.Phone,
                Address = domain.Address,
                City = domain.City,
                TaxNumber = domain.TaxNumber,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt
            };
        }

        public static void ApplyTo(this DomainSuppliers domain, EntitySuppliers entity)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Name = domain.Name;
            entity.ContactPerson = domain.ContactPerson;
            entity.Email = domain.Email;
            entity.Phone = domain.Phone;
            entity.Address = domain.Address;
            entity.City = domain.City;
            entity.TaxNumber = domain.TaxNumber;
            entity.UpdatedAt = DateTime.Now;
        }
    }
}
