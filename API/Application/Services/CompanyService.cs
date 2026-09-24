using Application.Authorization;
using Application.Interfaces;
using Application.Requests;
using Azure.Core;
using Domain.Helpers;
using Microsoft.Extensions.Logging;
using Persistence.Entities;
using Persistence.Interfaces;
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
        private readonly ICompanyAuthorizationService _companyAuthorizationService;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(
            IKeycloakAuthService keycloakAuthService,
            ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository,
            IClientRepository clientRepository,
            ICompanyAuthorizationService companyAuthorizationService,
            ILogger<CompanyService> logger)
        {
            _keycloakAuthService = keycloakAuthService;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _companyAuthorizationService = companyAuthorizationService;
            _logger = logger;
        }

        public async Task RegisterCompanyAsync(CompanyRegistration registration)
        {
            await _keycloakAuthService.AuthenticateAdminAsync();

            var user = UserMapper(
                registration.AdminEmployee.FirstName,
                registration.AdminEmployee.LastName,
                registration.AdminEmployee.Email,
                registration.AdminEmployee.Username,
                registration.AdminEmployee.Password);

            string keycloakUserId = null;

            try
            {
                keycloakUserId = await _keycloakAuthService.CreateUserAsync(user, false);

                await _keycloakAuthService.AssignRoleAsync(keycloakUserId, "owner");

                var comapny = new Companies
                {
                    Name = registration.CompanyName,
                    Email = registration.CompanyEmail,
                    Phone = registration.CompanyPhone,
                    Address = registration.CompanyAddress,
                    City = registration.CompanyCity,
                    TaxNumber = registration.CompanyTaxNumber,
                    Logo = registration.CompanyLogo,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                var adminEmployee = new Employee
                {
                    KeycloakUserId = keycloakUserId,
                    FirstName = registration.AdminEmployee.FirstName,
                    LastName = registration.AdminEmployee.LastName,
                    Email = registration.AdminEmployee.Email,
                    Phone = registration.AdminEmployee.Phone,
                    Position = registration.AdminEmployee.Position,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                };

                await _companyRepository.CrateCompanyWithAdminAsync(comapny, adminEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Company registration failed. Attempting to remove Keycloak user {UserId}", keycloakUserId);

                await RollbackUserAsync(keycloakUserId);

                throw;
            }
        }

        public async Task AddEmployeeToCompanyAsync(string keycloakUserId, EmployeeInsertRequest request)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var isAuthorized = await _companyAuthorizationService.IsUserInCompanyAsync(keycloakUserId, companyId);

            if (!isAuthorized)
                throw new Exception("User does not belong to this company.");

            var user = UserMapper(request.FirstName, request.LastName, request.Email, null, null);

            await _keycloakAuthService.AuthenticateAdminAsync();

            string employeeKeycloakId = null;

            try
            {
                employeeKeycloakId = await _keycloakAuthService.CreateUserAsync(user, true);

                await _keycloakAuthService.AssignRoleAsync(employeeKeycloakId, "employee");

                var newEmployee = new Employee
                {
                    KeycloakUserId = employeeKeycloakId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    Position = request.Position,
                    CompanyId = companyId
                };

                await _employeeRepository.CreateEmployeeAsync(newEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adding new employee failed. Attempting to remove Keycloak user {UserId}", employeeKeycloakId);

                await RollbackUserAsync(employeeKeycloakId);

                throw;
            }
        }

        public async Task AddClientToCompanyAsync(string keycloakUserId, ClientInsertRequest request)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var isAuthorized = await _companyAuthorizationService.IsUserInCompanyAsync(keycloakUserId, companyId);

            if (!isAuthorized)
                throw new Exception("User does not belong to this company.");

            var user = UserMapper(request.FirstName, request.LastName, request.Email, null, null);

            await _keycloakAuthService.AuthenticateAdminAsync();

            string clientKeycloakId = null;

            try
            {
                clientKeycloakId = await _keycloakAuthService.CreateUserAsync(user, true);

                await _keycloakAuthService.AssignRoleAsync(clientKeycloakId, "client");

                var newClient = new Client
                {
                    KeycloakUserId = clientKeycloakId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    Address = request.Address,
                    City = request.City,
                    Notes = request?.Notes,
                    CompanyId = companyId
                };

                await _clientRepository.CreateClientAsync(newClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adding new client failed. Attempting to remove Keycloak user {UserId}", clientKeycloakId);

                await RollbackUserAsync(clientKeycloakId);

                throw;
            }
        }

        private async Task RollbackUserAsync(string? keycloakUserId)
        {
            if (string.IsNullOrEmpty(keycloakUserId))
                return;

            try
            {
                await _keycloakAuthService.DeleteUserAsync(keycloakUserId);
                _logger.LogInformation("Compensation: deleted Keycloak user {UserId}", keycloakUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Keycloak user {UserId} after rollback. Schedule manual/automatic cleanup.", keycloakUserId);
            }
        }

        private User UserMapper(string firstName, string lastName, string email, string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                username = $"{firstName.ToLower()}_{lastName.ToLower()}";
                password = PasswordGenerator.GeneratePassword();
            }

            var user = new User
            {
                Username = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };

            return user;
        }
    }
}