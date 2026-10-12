using Application.Authorization;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Application.Validators;
using Azure.Core;
using Domain.Enums;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IKeycloakAuthService _keycloakAuthService;
        private readonly IAppointmentBookingValidator _bookingValidator;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(
            ICompanyRepository companyRepository,
            IClientRepository clientRepository,
            IServiceTypeRepository serviceTypeRepository,
            IAppointmentRepository appointmentRepository,
            IKeycloakAuthService keycloakAuthService,
            IAppointmentBookingValidator bookingValidator,
            ILogger<AppointmentService> logger)
        {
            _companyRepository = companyRepository;
            _clientRepository = clientRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _appointmentRepository = appointmentRepository;
            _keycloakAuthService = keycloakAuthService;
            _bookingValidator = bookingValidator;
            _logger = logger;
        }

        public async Task CreatePublicAppointmentAsync(string companySlug, PublicAppointmentRequest request, CancellationToken cancellationToken)
        {
            if (request.ServiceTypeId == null || request.ServiceTypeId.Count == 0)
                throw new BadRequestException("At least one service type must be provided.");

            var company = await _companyRepository.GetCompanyBySlugAsync(companySlug, cancellationToken)
                ?? throw new NotFoundException($"Company with slug - {companySlug} not found.");

            var client = await _clientRepository.FindByEmailAsync(request.Client.Email, company.Id, cancellationToken);

            if (client != null)
                throw new NotFoundException($"The user account already exists {client.Email}.");

            var serviceTypes = await _serviceTypeRepository
                .FindByIdsAsync(request.ServiceTypeId, company.Id, cancellationToken);

            if (serviceTypes.Count > 0 && (serviceTypes.Count != request.ServiceTypeId.Count))
                throw new NotFoundException("One or more requested service types were not found for this company.");

            var totalEstimatedMinutes = serviceTypes.Sum(s => s.EstimatedDuration);

            var employeeId = await _bookingValidator.ValidateAsync(
                company.ToDomain(),
                request.ScheduledAt,
                totalEstimatedMinutes,
                cancellationToken);

            var user = UserMapper.MapToUser(request.Client.FirstName, request.Client.LastName, request.Client.Email, null, null);

            var clientDomain = request.Client.ToDomain();
            clientDomain.CompanyId = company.Id;

            var vehicleDomain = request.Vehicle.ToDomain();
            vehicleDomain.CompanyId = company.Id;

            var appointmentDomain = request.ToDomain();
            appointmentDomain.Status = AppointmentStatus.Scheduled;
            appointmentDomain.CompanyId = company.Id;

            var serviceTypesDomain = serviceTypes.ToDomain();

            await CreateAppointment(
                clientDomain,
                vehicleDomain,
                appointmentDomain,
                serviceTypesDomain,
                user,
                employeeId,
                totalEstimatedMinutes,
                cancellationToken);
        }

        private async Task CreateAppointment(
            Client client,
            Vehicles vehicle,
            Appointments appointment,
            List<ServiceType> serviceTypes,
            User user,
            int employeeId,
            int totalEstimatedMinutes,
            CancellationToken cancellationToken)
        {
            await _keycloakAuthService.AuthenticateAdminAsync(cancellationToken);

            string keycloakUserId = null;

            try
            {
                keycloakUserId = await _keycloakAuthService.CreateUserAsync(user, true, cancellationToken);

                await _keycloakAuthService.AssignRoleAsync(keycloakUserId, "client", cancellationToken);

                client.KeycloakUserId = keycloakUserId;

                await _appointmentRepository.SaveAsync(
                    client.ToEntity(),
                    vehicle.ToEntity(),
                    appointment.ToEntity(totalEstimatedMinutes),
                    serviceTypes.ToExistingEntity(),
                    employeeId,
                    cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adding new client failed. Attempting to remove Keycloak user {UserId}", keycloakUserId);

                await _keycloakAuthService.RollbackUserAsync(keycloakUserId);

                throw;
            }
        }
    }

}
