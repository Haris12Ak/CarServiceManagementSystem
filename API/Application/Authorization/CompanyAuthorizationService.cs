using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authorization
{
    public class CompanyAuthorizationService : ICompanyAuthorizationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanyAuthorizationService(
            IEmployeeRepository employeeRepository,
            IClientRepository clientRepository,
            ICompanyRepository companyRepository)
        {
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
        }

        public async Task<int> GetCurrentUserCompanyIdAsync(string keycloakUserId)
        {
            var employeeCompanyId = await _employeeRepository.GetCompanyId(keycloakUserId);

            if (employeeCompanyId.HasValue)
                return employeeCompanyId.Value;

            var clientCompanyId = await _clientRepository.GetCompanyId(keycloakUserId);

            if (clientCompanyId.HasValue)
                return clientCompanyId.Value;

            throw new Exception("User does not belong to any company.");
        }

        public async Task<bool> IsUserInCompanyAsync(string keycloakUserId, int companyId)
        {
            return await _companyRepository.IsUserInCompanyAsync(keycloakUserId, companyId);
        }
    }
}
