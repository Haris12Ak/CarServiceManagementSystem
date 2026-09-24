using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int?> GetCompanyId(string keycloakUserId)
        {
            var companyId = await _context.Client
                .Where(x => x.KeycloakUserId == keycloakUserId && x.IsActive == true)
                .Select(x => (int?)x.CompanyId)
                .FirstOrDefaultAsync();

            return companyId;
        }

        public async Task<Client> FindClientByCompanyIdAsync(int companyId, string keycloakUserId)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.KeycloakUserId == keycloakUserId &&
                x.IsActive == true);

            if (client == null)
                return null;

            return client;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            await _context.Client.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

    }
}
