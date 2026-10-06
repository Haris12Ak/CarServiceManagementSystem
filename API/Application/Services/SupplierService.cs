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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public SupplierService(
            ISupplierRepository supplierRepository,
            ICurrentSystemUserService currentSystemUserService)
        {
            _supplierRepository = supplierRepository;
            _currentSystemUserService = currentSystemUserService;
        }

        public async Task<List<Suppliers>> GetAllSuppliersTypeAsync()
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var suppliers = await _supplierRepository.GetAllAsync(companyId);

            return suppliers.ToDomain();
        }

        public async Task<Suppliers> GetSupplierByIdAsync(int id)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var supplier = await _supplierRepository.FindByIdAsync(id, companyId);

            if (supplier == null)
                throw new NotFoundException($"Supplier with id {id} not found.");

            return supplier.ToDomain();
        }

        public async Task<Suppliers> AddSupplierAsync(SupplierRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var supplier = request.ToDomain(companyId);

            var entity = await _supplierRepository.SaveAsync(supplier.ToEntity());

            return entity.ToDomain();
        }

        public async Task<Suppliers> UpdateSupplierAsync(int id, SupplierRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var entity = await _supplierRepository.FindByIdAsync(id, companyId);

            if (entity == null)
                throw new NotFoundException($"Supplier with id {id} not found.");

            var domain = entity.ToDomain();

            request.ApplyTo(domain);

            domain.ApplyTo(entity);

            var updated = await _supplierRepository.SaveAsync(entity);

            return updated.ToDomain();
        }
    }
}
