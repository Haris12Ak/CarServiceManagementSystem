using Application.Authorization;
using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
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

            var serviceTypes = await _serviceTypeService.GetAllAsync(currentUser);

            return serviceTypes.ToDto();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServiceTypeDto>> GetById(int id)
        {
            var currentUser = _currentSystemUserService.KeycloakUserId;

            try
            {
                var serviceTypes = await _serviceTypeService.GetByIdAsync(id, currentUser);
                return serviceTypes.ToDto();
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}
