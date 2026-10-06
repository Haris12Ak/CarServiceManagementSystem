using Application.DTOs;
using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class SparePartsDtoMapper
    {
        public static SparePartsDto ToDto(this SpareParts domain)
        {
            return new SparePartsDto
            {
                Id = domain.Id,
                CompanyId = domain.CompanyId,
                SupplierId = domain.SupplierId,
                PartNumber = domain.PartNumber,
                Name = domain.Name,
                Description = domain.Description,
                PurchasePrice = domain.PurchasePrice,
                SellingPrice = domain.SellingPrice,
                MinimumStock = domain.MinimumStock,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt
            };
        }

        public static List<SparePartsDto> ToDto(this IEnumerable<SpareParts> domains)
        {
            return domains.Select(domain => domain.ToDto()).ToList();
        }

        public static SpareParts ToDomain(this SparePartsRequest request, int supplierId, int companyId)
        {
            return new SpareParts
            {
                CompanyId = companyId,
                SupplierId = supplierId,
                PartNumber = request.PartNumber,
                Name = request.Name,
                Description = request.Description,
                PurchasePrice = request.PurchasePrice,
                SellingPrice = request.SellingPrice,
                MinimumStock = request.MinimumStock,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static void ApplyTo(this SparePartsRequest request, SpareParts domain)
        {
            domain.SupplierId = request.SupplierId;
            domain.PartNumber = request.PartNumber;
            domain.Name = request.Name;
            domain.Description = request.Description;
            domain.PurchasePrice = request.PurchasePrice;
            domain.SellingPrice = request.SellingPrice;
            domain.MinimumStock = request.MinimumStock;
        }
    }
}
