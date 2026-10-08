using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ClientRepository : EfRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context) { }

        public async Task<int?> GetCompanyId(string keycloakUserId, CancellationToken cancellationToken)
        {
            var companyId = await _context.Client
                .Where(x => x.KeycloakUserId == keycloakUserId && x.IsActive == true)
                .Select(x => (int?)x.CompanyId)
                .FirstOrDefaultAsync(cancellationToken);

            return companyId;
        }

        public async Task<Client> FindByKeycloakId(string keycloakUserId, int comapnyId, CancellationToken cancellationToken)
        {
            var client = await _context.Client
                .AsNoTracking()
                .Where(x => x.KeycloakUserId == keycloakUserId &&
                x.CompanyId == comapnyId &&
                x.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);

            if (client == null)
                return null;

            return client;
        }

    }
}
