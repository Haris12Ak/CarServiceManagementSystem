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
        private readonly IClientRepository _clientRepository;

        public VehicleService(
            IVehicleRepository vehicleRepository,
            ICurrentSystemUserService currentSystemUserService,
            IClientRepository clientRepository)
        {
            _vehicleRepository = vehicleRepository;
            _currentSystemUserService = currentSystemUserService;
            _clientRepository = clientRepository;
        }

        public async Task<List<Vehicles>> GetAllVehiclesAsync(CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var vehicles = await _vehicleRepository.GetAllAsync(companyId, cancellationToken);

            return vehicles.ToDomain();
        }
        public async Task<List<Vehicles>> GetVehiclesByClientIdAsync(int clientId, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var vehicles = await _vehicleRepository.GetByClientIdAsync(clientId, companyId, cancellationToken);

            return vehicles.ToDomain();
        }

        public async Task<Vehicles> GetVehicleById(int id, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var vehicle = await _vehicleRepository.FindByIdAsync(id, companyId, cancellationToken)
                ?? throw new NotFoundException($"Vehicle with id {id} not found.");

            return vehicle.ToDomain();
        }

        public async Task<Vehicles> AddVehicleAsync(VehicleRequest request, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var client = await _clientRepository.FindByIdAsync(request.ClientId, companyId, cancellationToken)
                ?? throw new NotFoundException($"Client with id {request.ClientId} not found.");

            var vehicle = request.ToDomain(client.Id, companyId);

            var entity = await _vehicleRepository.SaveAsync(vehicle.ToEntity(), cancellationToken);

            return entity.ToDomain();
        }

        public async Task<Vehicles> UpdateVehicleAsync(int id, VehicleRequest request, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var entity = await _vehicleRepository.FindByIdAsync(id, companyId, cancellationToken)
                ?? throw new NotFoundException($"Vehicle with id {id} not found.");

            _ = await _clientRepository.FindByIdAsync(request.ClientId, companyId, cancellationToken)
                ?? throw new NotFoundException($"Client with id {request.ClientId} not found.");

            var domain = entity.ToDomain();

            request.ApplyTo(domain);

            domain.ApplyTo(entity);

            var updated = await _vehicleRepository.SaveAsync(entity, cancellationToken);

            return updated.ToDomain();
        }

    }
}
