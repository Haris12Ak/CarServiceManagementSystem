using Application.Authorization;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class SparePartsService : ISparePartsService
    {
        private readonly ISparePartsRepository _sparePartsRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public SparePartsService(
            ISparePartsRepository sparePartsRepository,
            ICurrentSystemUserService currentSystemUserService,
            ISupplierRepository supplierRepository,
            IEmployeeRepository employeeRepository)
        {
            _sparePartsRepository = sparePartsRepository;
            _currentSystemUserService = currentSystemUserService;
            _supplierRepository = supplierRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<SpareParts>> GetAllSparePartsAsync()
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var spareParts = await _sparePartsRepository.GetAllAsync(companyId);

            return spareParts.ToDomain();
        }

        public async Task<SpareParts> GetSparePartsByIdAsync(int id)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var sparePart = await _sparePartsRepository.FindByIdAsync(id, companyId)
                ?? throw new NotFoundException($"SparePart with id {id} not found.");

            return sparePart.ToDomain();
        }

        public async Task<SpareParts> AddSparePartsAsync(SparePartsRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var keycloakUserId = _currentSystemUserService.KeycloakUserId;

            var isExists = await _sparePartsRepository.IsExistAsync(request.PartNumber, companyId);

            if (isExists)
                throw new BadRequestException($"The part {request.Name} with the entered partNumber - {request.PartNumber}, already exists in the database.");

            var supplier = await _supplierRepository.FindByIdAsync(request.SupplierId, companyId)
                ?? throw new NotFoundException($"Supplier with id {request.SupplierId} not found.");

            var employee = await _employeeRepository.FindByKeycloakIdAsync(keycloakUserId, companyId)
                ?? throw new NotFoundException($"Employee with keycloakUserId {keycloakUserId} not found.");

            var sparePart = request.ToDomain(supplier.Id, companyId);

            var entity = await _sparePartsRepository.SaveAsync(
                sparePart.ToEntity(),
                request.InitialQuantity,
                employee.Id);

            return entity.ToDomain();
        }

        public async Task<SpareParts> UpdateSparePartsAsync(int id, SparePartsRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var entity = await _sparePartsRepository.FindByIdAsync(id, companyId)
                ?? throw new NotFoundException($"Spare part with id {id} not found.");

            _ = await _supplierRepository.FindByIdAsync(request.SupplierId, companyId)
                ?? throw new NotFoundException($"Supplier with id {request.SupplierId} not found.");

            var domain = entity.ToDomain();

            request.ApplyTo(domain);

            domain.ApplyTo(entity);

            var updated = await _sparePartsRepository.SaveAsync(entity);

            return updated.ToDomain();
        }
    }
}
