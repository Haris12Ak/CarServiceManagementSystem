using Application.DTOs;
using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class ServiceTypeDtoMapper
    {
        public static ServiceTypeDto ToDto(this ServiceType domain)
        {
            return new ServiceTypeDto
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

        public static List<ServiceTypeDto> ToDto(this IEnumerable<ServiceType> domains)
        {
            return domains.Select(domain => domain.ToDto()).ToList();
        }

        public static ServiceType ToDomain(this ServiceTypeRequest request, int companyId)
        {
            return new ServiceType
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                EstimatedDuration = request.EstimatedDuration,
                CompanyId = companyId,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static void UpdateDomain(this ServiceTypeRequest request, ServiceType domain)
        {
            domain.Name = request.Name;
            domain.Description = request.Description;
            domain.Price = request.Price;
            domain.EstimatedDuration = request.EstimatedDuration;
        }
    }
}
