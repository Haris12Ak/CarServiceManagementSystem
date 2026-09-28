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
                Email = domain.Email,
                Phone = domain.Phone,
                Address = domain.Address,
                City = domain.City,
                TaxNumber = domain.TaxNumber,
                Logo = domain.Logo,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt
            };
        }
    }
}
