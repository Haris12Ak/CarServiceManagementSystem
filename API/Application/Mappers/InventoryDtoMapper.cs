using Application.DTOs;
using Application.Requests;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class InventoryDtoMapper
    {
        public static InventoryDto ToDto(this Inventory inventory)
        {
            return new InventoryDto
            {
                Id = inventory.Id,
                CompanyId = inventory.CompanyId,
                SparePartId = inventory.SparePartId,
                Quantity = inventory.Quantity,
                StockStatus = StockStatusHelper.GetStockStatus(
                    inventory.Quantity,
                    inventory.SpareParts.MinimumStock),
                UpdatedAt = inventory.UpdatedAt
            };
        }

        public static List<InventoryTransactionsDto> ToDto(this IEnumerable<InventoryTransactions> domains)
        {
            return domains
                .Select(domain =>
                new InventoryTransactionsDto
                {
                    Id = domain.Id,
                    CompanyId = domain.CompanyId,
                    SparePartId = domain.SparePartId,
                    EmployeeId = domain.EmployeeId,
                    Type = domain.Type,
                    Quantity = domain.Quantity,
                    ReferenceType = domain.ReferenceType,
                    ReferenceId = domain.ReferenceId,
                    CreatedAt = domain.CreatedAt,
                    Note = domain.Note
                }).ToList();
        }

    }
}
