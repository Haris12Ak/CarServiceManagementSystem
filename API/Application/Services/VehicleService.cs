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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public VehicleService(
            IVehicleRepository vehicleRepository,
            ICurrentSystemUserService currentSystemUserService)
        {
            _vehicleRepository = vehicleRepository;
            _currentSystemUserService = currentSystemUserService;
        }

        public async Task<List<Vehicles>> GetAllVehiclesAsync()
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var vehicles = await _vehicleRepository.GetAllAsync(companyId);

            return vehicles.ToDomain();
        }
        public async Task<List<Vehicles>> GetVehiclesByClientIdAsync(int clientId)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var vehicles = await _vehicleRepository.GetByClientIdAsync(clientId, companyId);

            return vehicles.ToDomain();
        }

        public async Task<Vehicles> GetVehicleById(int id)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var vehicle = await _vehicleRepository.FindByIdAsync(id, companyId);

            if (vehicle == null)
                throw new NotFoundException($"Vehicle with id {id} not found.");

            return vehicle.ToDomain();
        }

        public async Task<Vehicles> AddVehicleAsync(VehicleRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var vehicle = request.ToDomain(request.ClientId, companyId);

            var entity = await _vehicleRepository.SaveAsync(vehicle.ToEntity());

            return entity.ToDomain();
        }

        public async Task<Vehicles> UpdateVehicleAsync(int id, VehicleRequest request)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var entity = await _vehicleRepository.FindByIdAsync(id, companyId);

            if (entity == null)
                throw new NotFoundException($"Vehicle with id {id} not found.");

            var domain = entity.ToDomain();

            request.ApplyTo(domain);

            domain.ApplyTo(entity);

            var updated = await _vehicleRepository.SaveAsync(entity);

            return updated.ToDomain();
        }

    }
}
