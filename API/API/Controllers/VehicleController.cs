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
        public async Task<List<VehicleDto>> GetAll(CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync(cancellationToken);

            return vehicles.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<VehicleDto> GetById(int id, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleById(id, cancellationToken);

            return vehicle.ToDto();
        }

        [HttpGet("client/{clientId}")]
        public async Task<List<VehicleDto>> GetByClientId(int clientId, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetVehiclesByClientIdAsync(clientId, cancellationToken);

            return vehicles.ToDto();
        }

        [Authorize(Roles = "owner")]
        [HttpPost]
        public async Task<VehicleDto> Add([FromBody] VehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.AddVehicleAsync(request, cancellationToken);

            return vehicle.ToDto();
        }

        [Authorize(Roles = "owner")]
        [HttpPut("{id}")]
        public async Task<VehicleDto> Update(int id, [FromBody] VehicleRequest request, CancellationToken cancellationToken)
        {
            var serviceTypes = await _vehicleService.UpdateVehicleAsync(id, request, cancellationToken);

            return serviceTypes.ToDto();
        }
    }
}
