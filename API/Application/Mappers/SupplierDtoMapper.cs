using Application.DTOs;
using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class SupplierDtoMapper
    {
        public static SupplierDto ToDto(this Suppliers domain)
        {
            return new SupplierDto
            {
                Id = domain.Id,
                CompanyId = domain.CompanyId,
                Name = domain.Name,
                ContactPerson = domain.ContactPerson,
                Email = domain.Email,
                Phone = domain.Phone,
                Address = domain.Address,
                City = domain.City,
                TaxNumber = domain.TaxNumber,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt
            };
        }

        public static List<SupplierDto> ToDto(this IEnumerable<Suppliers> domains)
        {
            return domains.Select(domain => domain.ToDto()).ToList();
        }

        public static Suppliers ToDomain(this SupplierRequest request, int companyId)
        {
            return new Suppliers
            {
                CompanyId = companyId,
                Name = request.Name,
                ContactPerson = request.ContactPerson,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                TaxNumber = request.TaxNumber,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static void ApplyTo(this SupplierRequest request, Suppliers domain)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            domain.Name = request.Name;
            domain.ContactPerson = request.ContactPerson;
            domain.Email = request.Email;
            domain.Phone = request.Phone;
            domain.Address = request.Address;
            domain.City = request.City;
            domain.TaxNumber = request.TaxNumber;
        }
    }
}
