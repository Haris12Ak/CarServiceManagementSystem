using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<List<VehicleDto>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();

            return vehicles.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<VehicleDto> GetById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);

            return vehicle.ToDto();
        }

        [HttpGet("client/{clientId}")]
        public async Task<List<VehicleDto>> GetByClientId(int clientId)
        {
            var vehicles = await _vehicleService.GetVehiclesByClientIdAsync(clientId);

            return vehicles.ToDto();
        }

        [Authorize(Roles = "owner")]
        [HttpPost]
        public async Task<VehicleDto> Add([FromBody] VehicleRequest request)
        {
            var vehicle = await _vehicleService.AddVehicleAsync(request);

            return vehicle.ToDto();
        }

        [Authorize(Roles = "owner")]
        [HttpPut("{id}")]
        public async Task<VehicleDto> Update(int id, [FromBody] VehicleRequest request)
        {
            var serviceTypes = await _vehicleService.UpdateVehicleAsync(id, request);

            return serviceTypes.ToDto();
        }
    }
}
