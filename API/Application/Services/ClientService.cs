using Application.Authorization;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public ClientService(
            IClientRepository clientRepository,
            ICurrentSystemUserService currentSystemUserService)
        {
            _clientRepository = clientRepository;
            _currentSystemUserService = currentSystemUserService;
        }

        public async Task<List<Client>> GetAllClientAsync(CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var clients = await _clientRepository.GetAllAsync(companyId, cancellationToken);

            return clients.ToDomain();
        }

        public async Task<Client> GetClientByIdAsync(int id, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var client = await _clientRepository.FindByIdAsync(id, companyId, cancellationToken)
                ?? throw new NotFoundException($"Client with id {id} not found.");

            return client.ToDomain();
        }
    }
}
