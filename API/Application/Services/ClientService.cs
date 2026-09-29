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
        private readonly ICompanyAuthorizationService _companyAuthorizationService;

        public ClientService(
            IClientRepository clientRepository,
            ICompanyAuthorizationService companyAuthorizationService)
        {
            _clientRepository = clientRepository;
            _companyAuthorizationService = companyAuthorizationService;
        }

        public async Task<List<Client>> GetAllClientAsync(string keycloakUserId)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var clients = await _clientRepository.GetAllAsync(companyId);

            return clients.ToDomain();
        }

        public async Task<Client> GetClientByIdAsync(int id, string keycloakUserId)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var client = await _clientRepository.FindByIdAsync(id, companyId);

            if (client == null)
                throw new NotFoundException($"Client with id {id} not found.");

            return client.ToDomain();
        }
    }
}
