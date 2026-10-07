using Application.Authorization;
using Application.Interfaces;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentSystemUserService _currentSystemUserService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;

        public UserService(
            ICurrentSystemUserService currentSystemUserService,
            ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository,
            IClientRepository clientRepository)
        {
            _currentSystemUserService = currentSystemUserService;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
        }

        public async Task<(Employee? emoloyee, Client? client, Companies? company)> GetUserInfoAsync()
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync();

            var keycloakUserId = _currentSystemUserService.KeycloakUserId;

            var company = await _companyRepository.FindByIdAsync(companyId);

            if (company != null)
            {
                var companyDomain = company.ToDomain();

                var employee = await _employeeRepository.FindByKeycloakIdAsync(keycloakUserId, companyId);

                if (employee != null)
                {
                    var employeeDomain = employee.ToDomain();

                    return (employeeDomain, null, companyDomain);
                }
                else
                {
                    var client = await _clientRepository.FindByKeycloakId(keycloakUserId, companyId);

                    var clientDomain = client.ToDomain();

                    return (null, clientDomain, companyDomain);
                }
            }

            return (null, null, null);
        }
    }
}
