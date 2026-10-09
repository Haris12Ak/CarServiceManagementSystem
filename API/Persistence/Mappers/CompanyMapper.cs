using DomainCompany = Domain.Models.Companies;
using EntityCompany = Persistence.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappers
{
    public static class CompanyMapper
    {
        public static EntityCompany ToEntity(this DomainCompany domain)
        {
            if (domain == null)
                return null;

            return new EntityCompany
            {
                Name = domain.Name,
                Slug = domain.Slug,
                Email = domain.Email,
                Phone = domain.Phone,
                Address = domain.Address,
                City = domain.City,
                TaxNumber = domain.TaxNumber,
                Logo = domain.Logo,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt
            };
        }

        public static DomainCompany ToDomain(this EntityCompany entity)
        {
            return new DomainCompany
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Email = entity.Email,
                Phone = entity.Phone,
                Address = entity.Address,
                City = entity.City,
                TaxNumber = entity.TaxNumber,
                Logo = entity.Logo,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
