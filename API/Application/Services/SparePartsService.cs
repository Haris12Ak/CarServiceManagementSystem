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

        public SparePartsService(
            ISparePartsRepository sparePartsRepository,
            ICurrentSystemUserService currentSystemUserService,
            ISupplierRepository supplierRepository)
        {
            _sparePartsRepository = sparePartsRepository;
            _currentSystemUserService = currentSystemUserService;
            _supplierRepository = supplierRepository;
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

            var supplier = await _supplierRepository.FindByIdAsync(request.SupplierId, companyId)
                ?? throw new NotFoundException($"Supplier with id {request.SupplierId} not found.");

            var sparePart = request.ToDomain(supplier.Id, companyId);

            var entity = await _sparePartsRepository.SaveAsync(sparePart.ToEntity());

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
