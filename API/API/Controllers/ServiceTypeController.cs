using Application.Authorization;
using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Application.Services;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public ServiceTypeController(
            IServiceTypeService serviceTypeService,
            ICurrentSystemUserService currentSystemUserService)
        {
            _serviceTypeService = serviceTypeService;
            _currentSystemUserService = currentSystemUserService;
        }

        [HttpGet]
        public async Task<List<ServiceTypeDto>> GetAll()
        {
            var currentUser = _currentSystemUserService.KeycloakUserId;

            var serviceTypes = await _serviceTypeService.GetAllServiceTypeAsync(currentUser);

            return serviceTypes.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<ServiceTypeDto> GetById(int id)
        {
            var currentUser = _currentSystemUserService.KeycloakUserId;

            var serviceTypes = await _serviceTypeService.GetServiceTypeByIdAsync(id, currentUser);

            return serviceTypes.ToDto();
        }

        [HttpPost]
        public async Task<ServiceTypeDto> Add(ServiceTypeRequest request)
        {
            var currentUser = _currentSystemUserService.KeycloakUserId;

            var serviceTypes = await _serviceTypeService.AddServiceTypeAsync(currentUser, request);

            return serviceTypes.ToDto();
        }

        [HttpPut("{id}")]
        public async Task<ServiceTypeDto> Update(int id, ServiceTypeRequest request)
        {
            var currentUser = _currentSystemUserService.KeycloakUserId;

            var serviceTypes = await _serviceTypeService.UpdateServiceTypeAsync(id, currentUser, request);

            return serviceTypes.ToDto();
        }
    }
}
