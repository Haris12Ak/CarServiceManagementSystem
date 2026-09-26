using Application.Authorization;
using Application.Interfaces;
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
                throw new Exception($"Service type with id {id} not found.");

            return serviceType.ToDomain();
        }
    }
}
