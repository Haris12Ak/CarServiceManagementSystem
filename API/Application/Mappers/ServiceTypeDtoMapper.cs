using Application.DTOs;
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
    }
}
