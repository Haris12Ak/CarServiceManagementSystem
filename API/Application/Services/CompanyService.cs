using Application.Authorization;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Azure.Core;
using Domain.Helpers;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Persistence.Interfaces;
using Persistence.Mappers;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IKeycloakAuthService _keycloakAuthService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICurrentSystemUserService _currentSystemUserService;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(
            IKeycloakAuthService keycloakAuthService,
            ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository,
            IClientRepository clientRepository,
            ICurrentSystemUserService currentSystemUserService,
            ILogger<CompanyService> logger)
        {
            _keycloakAuthService = keycloakAuthService;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _currentSystemUserService = currentSystemUserService;
            _logger = logger;
        }

        public async Task RegisterCompanyAsync(CompanyRegistration registration, CancellationToken cancellationToken)
        {
            await _keycloakAuthService.AuthenticateAdminAsync(cancellationToken);

            var user = UserMapper.MapToUser(
                registration.AdminEmployee.FirstName,
                registration.AdminEmployee.LastName,
                registration.AdminEmployee.Email,
                registration.AdminEmployee.Username,
                registration.AdminEmployee.Password);

            string keycloakUserId = null;

            try
            {
                keycloakUserId = await _keycloakAuthService.CreateUserAsync(user, false, cancellationToken);

                await _keycloakAuthService.AssignRoleAsync(keycloakUserId, "owner", cancellationToken);

                var slug = await GenerateUniqueSlugAsync(registration.CompanyName, cancellationToken);

                var company = CompanyDtoMapper.ToDomain(registration, slug);

                var adminEmployee = EmployeeDtoMapper.ToDomain(registration, keycloakUserId);

                await _companyRepository.CrateCompanyWithAdminAsync(company.ToEntity(), adminEmployee.ToEntity(), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Company registration failed. Attempting to remove Keycloak user {UserId}", keycloakUserId);

                await RollbackUserAsync(keycloakUserId, cancellationToken);

                throw;
            }
        }

        public async Task AddEmployeeToCompanyAsync(EmployeeInsertRequest request, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var user = UserMapper.MapToUser(request.FirstName, request.LastName, request.Email, null, null);

            await _keycloakAuthService.AuthenticateAdminAsync(cancellationToken);

            string keycloakUserId = null;

            try
            {
                keycloakUserId = await _keycloakAuthService.CreateUserAsync(user, true, cancellationToken);

                await _keycloakAuthService.AssignRoleAsync(keycloakUserId, "employee", cancellationToken);

                var employee = request.ToDomain(keycloakUserId, companyId);

                await _employeeRepository.SaveAsync(employee.ToEntity(), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adding new employee failed. Attempting to remove Keycloak user {UserId}", keycloakUserId);

                await RollbackUserAsync(keycloakUserId, cancellationToken);

                throw;
            }
        }

        public async Task AddClientToCompanyAsync(ClientInsertRequest request, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var user = UserMapper.MapToUser(request.FirstName, request.LastName, request.Email, null, null);

            await _keycloakAuthService.AuthenticateAdminAsync(cancellationToken);

            string keycloakUserId = null;

            try
            {
                keycloakUserId = await _keycloakAuthService.CreateUserAsync(user, true, cancellationToken);

                await _keycloakAuthService.AssignRoleAsync(keycloakUserId, "client", cancellationToken);

                var client = request.ToDomain(keycloakUserId, companyId);

                await _clientRepository.SaveAsync(client.ToEntity(), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adding new client failed. Attempting to remove Keycloak user {UserId}", keycloakUserId);

                await RollbackUserAsync(keycloakUserId, cancellationToken);

                throw;
            }
        }

        private async Task RollbackUserAsync(string? keycloakUserId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(keycloakUserId))
                return;

            try
            {
                await _keycloakAuthService.DeleteUserAsync(keycloakUserId, cancellationToken);
                _logger.LogInformation("Compensation: deleted Keycloak user {UserId}", keycloakUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Keycloak user {UserId} after rollback. Schedule manual/automatic cleanup.", keycloakUserId);
            }
        }

        private async Task<string> GenerateUniqueSlugAsync(string companyName, CancellationToken cancellationToken)
        {
            var baseSlug = SlugHelper.Generate(companyName);
            var slug = baseSlug;
            var suffix = 2;

            while (await _companyRepository.SlugExistsAsync(slug, cancellationToken))
            {
                slug = $"{baseSlug}-{suffix}";
                suffix++;
            }

            return slug;
        }
    }
}