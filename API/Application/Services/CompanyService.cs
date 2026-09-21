using Application.Interfaces;
using Application.Requests;
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
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(IKeycloakAuthService keycloakAuthService,
            ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository,
            ILogger<CompanyService> logger)
        {
            _keycloakAuthService = keycloakAuthService;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task RegisterCompanyAsync(CompanyRegistration registration)
        {
            await _keycloakAuthService.AuthenticateAdminAsync();

            var user = new User
            {
                Username = registration.AdminEmployee.Username,
                Email = registration.AdminEmployee.Email,
                FirstName = registration.AdminEmployee.FirstName,
                LastName = registration.AdminEmployee.LastName,
                Password = registration.AdminEmployee.Password
            };

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

        public async Task AddEmployeeToCompanyAsync(int companyId, string keycloakUserId, EmployeeInsertRequest request)
        {
            var owner = await _employeeRepository.FindEmployeeByCompanyIdAsync(companyId, keycloakUserId)
                ?? throw new Exception($"Owner with ID {keycloakUserId} not found.");

            if (owner.CompanyId != companyId)
                throw new Exception($"Owner with ID {keycloakUserId} does not belong to company with ID {companyId}.");

            var username = $"{request.FirstName.ToLower()}_{request.LastName.ToLower()}";
            var password = PasswordGenerator.GeneratePassword();

            var user = new User
            {
                Username = username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = password
            };

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
                    CompanyId = owner.CompanyId
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
    }
}