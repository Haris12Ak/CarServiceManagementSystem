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

        public async Task<Inventory> GetInventoryBySparePartIdAsync(int sparePartId, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var inventory = await _inventoryRepository.FindBySparePartIdAsync(sparePartId, companyId, cancellationToken)
                ?? throw new NotFoundException($"Inventory with sparePartId {sparePartId} not found.");

            return inventory.ToDomain();
        }

        public async Task<Inventory> AddStockAsync(int sparePartId, InventoryRequest request, CancellationToken cancellationToken)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.In,
                InventoryTransactionReferenceType.Purchase,
                cancellationToken);
        }

        public async Task<Inventory> AdjustInAsync(int sparePartId, InventoryRequest request, CancellationToken cancellationToken)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.AdjustmentIn,
                InventoryTransactionReferenceType.Adjustment,
                cancellationToken);
        }

        public async Task<Inventory> AdjustOutAsync(int sparePartId, InventoryRequest request, CancellationToken cancellationToken)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.AdjustmentOut,
                InventoryTransactionReferenceType.Adjustment,
                cancellationToken);
        }

        public async Task<Inventory> RemoveStockAsync(int sparePartId, int workOrderId, InventoryRequest request, CancellationToken cancellationToken)
        {
            return await ChangeQuantityAsync(sparePartId,
                request,
                InventoryTransactionType.Out,
                InventoryTransactionReferenceType.WorkOrder,
                cancellationToken,
                workOrderId);
        }

        public async Task<List<InventoryTransactions>> GetInventoryTransactionAsync(int sparePartId, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            _ = await _inventoryRepository.FindBySparePartIdAsync(sparePartId, companyId, cancellationToken)
                ?? throw new NotFoundException($"Inventory with sparePartId {sparePartId} not found.");

            var inventoryTransactions = await _inventoryRepository.GetInventoryTransactionsBySparePartId(sparePartId, companyId, cancellationToken);

            return inventoryTransactions.ToDomain();
        }

        private async Task<Inventory> ChangeQuantityAsync(
            int sparePartId,
            InventoryRequest request,
            InventoryTransactionType type,
            InventoryTransactionReferenceType referenceType,
            CancellationToken cancellationToken,
            int? referenceId = null)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);
            var keycloakUserId = _currentSystemUserService.KeycloakUserId;

            var inventory = await _inventoryRepository.FindBySparePartIdAsync(sparePartId, companyId, cancellationToken)
                ?? throw new NotFoundException($"Inventory with sparePartId {sparePartId} not found.");

            var employee = await _employeeRepository.FindByKeycloakIdAsync(keycloakUserId, companyId, cancellationToken)
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

            var updated = await _inventoryRepository.SaveAsync(inventory, inventoryTransactions.ToEntity(), cancellationToken);

            return updated.ToDomain();
        }
    }
}
