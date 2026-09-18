using Application.Interfaces;
using Application.Requests;
using Microsoft.Extensions.Logging;
using Persistence.Entities;
using Persistence.Interfaces;
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
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(IKeycloakAuthService keycloakAuthService, ICompanyRepository companyRepository, ILogger<CompanyService> logger)
        {
            _keycloakAuthService = keycloakAuthService;
            _companyRepository = companyRepository;
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

                if (!string.IsNullOrEmpty(keycloakUserId))
                {
                    try
                    {
                        await _keycloakAuthService.DeleteUserAsync(keycloakUserId);
                        _logger.LogInformation("Compensation: deleted Keycloak user {UserId}", keycloakUserId);
                    }
                    catch (Exception delEx)
                    {
                        _logger.LogError(delEx, "Failed to delete Keycloak user {UserId} after rollback. Schedule manual/automatic cleanup.", keycloakUserId);
                    }
                }

                throw;
            }
        }
    }
}