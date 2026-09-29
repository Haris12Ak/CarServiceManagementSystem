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
        private string CurrentUserId => _currentSystemUserService.KeycloakUserId;

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
            var serviceTypes = await _serviceTypeService.GetAllServiceTypeAsync(CurrentUserId);

            return serviceTypes.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<ServiceTypeDto> GetById(int id)
        {
            var serviceTypes = await _serviceTypeService.GetServiceTypeByIdAsync(id, CurrentUserId);

            return serviceTypes.ToDto();
        }

        [HttpPost]
        public async Task<ServiceTypeDto> Add([FromBody] ServiceTypeRequest request)
        {
            var serviceTypes = await _serviceTypeService.AddServiceTypeAsync(CurrentUserId, request);

            return serviceTypes.ToDto();
        }

        [HttpPut("{id}")]
        public async Task<ServiceTypeDto> Update(int id, [FromBody] ServiceTypeRequest request)
        {
            var serviceTypes = await _serviceTypeService.UpdateServiceTypeAsync(id, CurrentUserId, request);

            return serviceTypes.ToDto();
        }
    }
}
