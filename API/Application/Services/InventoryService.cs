using Application.Authorization;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;
        private readonly IEmployeeRepository _employeeRepository;

        public InventoryService(
            IInventoryRepository inventoryRepository,
            ICurrentSystemUserService currentSystemUserService,
            IEmployeeRepository employeeRepository)
        {
            _inventoryRepository = inventoryRepository;
            _currentSystemUserService = currentSystemUserService;
            _employeeRepository = employeeRepository;
        }

        public async Task<Inventory> GetInventoryBySparePartIdAsync(int sparePartId)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var inventory = await _inventoryRepository.FindBySparePartIdAsync(sparePartId, companyId)
                ?? throw new NotFoundException($"Inventory with sparePartId {sparePartId} not found.");

            return inventory.ToDomain();
        }

        public async Task<Inventory> AddStockAsync(int sparePartId, InventoryRequest request)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.In,
                InventoryTransactionReferenceType.Purchase);
        }

        public async Task<Inventory> AdjustInAsync(int sparePartId, InventoryRequest request)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.AdjustmentIn,
                InventoryTransactionReferenceType.Adjustment);
        }

        public async Task<Inventory> AdjustOutAsync(int sparePartId, InventoryRequest request)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.AdjustmentOut,
                InventoryTransactionReferenceType.Adjustment);
        }

        public async Task<Inventory> RemoveStockAsync(int sparePartId, int workOrderId, InventoryRequest request)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.Out,
                InventoryTransactionReferenceType.WorkOrder,
                workOrderId);
        }

        public async Task<List<InventoryTransactions>> GetInventoryTransactionAsync(int sparePartId)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            _ = await _inventoryRepository.FindBySparePartIdAsync(sparePartId, companyId)
                ?? throw new NotFoundException($"Inventory with sparePartId {sparePartId} not found.");

            var inventoryTransactions = await _inventoryRepository.GetInventoryTransactionsBySparePartId(sparePartId, companyId);

            return inventoryTransactions.ToDomain();
        }

        private async Task<Inventory> ChangeQuantityAsync(
            int sparePartId,
            InventoryRequest request,
            InventoryTransactionType type,
            InventoryTransactionReferenceType referenceType,
            int? referenceId = null)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();
            var keycloakUserId = _currentSystemUserService.KeycloakUserId;

            var inventory = await _inventoryRepository.FindBySparePartIdAsync(sparePartId, companyId)
                ?? throw new NotFoundException($"Inventory with sparePartId {sparePartId} not found.");

            var employee = await _employeeRepository.FindByKeycloakIdAsync(keycloakUserId, companyId)
                ?? throw new NotFoundException($"Employee with keycloakUserId {keycloakUserId} not found.");

            switch (type)
            {
                case InventoryTransactionType.In:
                case InventoryTransactionType.AdjustmentIn:

                    inventory.Quantity += request.Quantity;

                    break;
                case InventoryTransactionType.Out:
                case InventoryTransactionType.AdjustmentOut:

                    if (inventory.Quantity < request.Quantity)
                        throw new BadRequestException("There is not enough stock.");

                    inventory.Quantity -= request.Quantity;

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            inventory.UpdatedAt = DateTime.Now;

            var inventoryTransactions = new InventoryTransactions
            {
                Type = type,
                Quantity = request.Quantity,
                ReferenceType = referenceType,
                ReferenceId = referenceId,
                CreatedAt = DateTime.Now,
                Note = request.Note,
                CompanyId = companyId,
                SparePartId = sparePartId,
                EmployeeId = employee.Id
            };

            var updated = await _inventoryRepository.SaveAsync(inventory, inventoryTransactions.ToEntity());

            return updated.ToDomain();
        }
    }
}
