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
        private readonly ICompanyAuthorizationService _companyAuthorizationService;

        public ServiceTypeService(
            IServiceTypeRepository serviceTypeRepository,
            ICompanyAuthorizationService companyAuthorizationService)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _companyAuthorizationService = companyAuthorizationService;
        }


        public async Task<List<ServiceType>> GetAllAsync(string keycloakUserId)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var serviceTypes = await _serviceTypeRepository.FindAllAsync(companyId);

            return serviceTypes.ToDomain();
        }

        public async Task<ServiceType> GetByIdAsync(int id, string keycloakUserId)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var serviceType = await _serviceTypeRepository.FindByIdAsync(id, companyId);

            if (serviceType == null)
                throw new NotFoundException($"Service type with id {id} not found.");

            return serviceType.ToDomain();
        }

        public async Task<ServiceType> AddAsync(string keycloakUserId, ServiceTypeRequest request)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var serviceType = request.ToDomain(companyId);

            var entity = await _serviceTypeRepository.CreateAsync(serviceType.ToEntity());

            return entity.ToDomain();
        }

        public async Task<ServiceType> UpdateAsync(int id, string keycloakUserId, ServiceTypeRequest request)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var entity = await _serviceTypeRepository.FindByIdAsync(id, companyId);

            if (entity == null)
                throw new NotFoundException($"Service type with id {id} not found.");

            var domain = entity.ToDomain();

            request.UpdateDomain(domain);

            domain.MapToExistingEntity(entity);

            var updated = await _serviceTypeRepository.UpdateAsync(entity);

            return updated.ToDomain();
        }
    }
}
