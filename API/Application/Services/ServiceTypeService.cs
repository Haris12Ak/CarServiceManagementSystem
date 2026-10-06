using Application.Authorization;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public ServiceTypeService(
            IServiceTypeRepository serviceTypeRepository,
            ICurrentSystemUserService currentSystemUserService)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _currentSystemUserService = currentSystemUserService;
        }


        public async Task<List<ServiceType>> GetAllServiceTypeAsync()
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var serviceTypes = await _serviceTypeRepository.GetAllAsync(companyId);

            return serviceTypes.ToDomain();
        }

        public async Task<ServiceType> GetServiceTypeByIdAsync(int id)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var serviceType = await _serviceTypeRepository.FindByIdAsync(id, companyId)
                ?? throw new NotFoundException($"Service type with id {id} not found.");

            return serviceType.ToDomain();
        }

        public async Task<ServiceType> AddServiceTypeAsync(ServiceTypeRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var serviceType = request.ToDomain(companyId);

            var entity = await _serviceTypeRepository.SaveAsync(serviceType.ToEntity());

            return entity.ToDomain();
        }

        public async Task<ServiceType> UpdateServiceTypeAsync(int id, ServiceTypeRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var entity = await _serviceTypeRepository.FindByIdAsync(id, companyId)
                ?? throw new NotFoundException($"Service type with id {id} not found.");

            var domain = entity.ToDomain();

            request.ApplyTo(domain);

            domain.ApplyTo(entity);

            var updated = await _serviceTypeRepository.SaveAsync(entity);

            return updated.ToDomain();
        }
    }
}
