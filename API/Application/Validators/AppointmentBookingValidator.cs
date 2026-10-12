using Application.Exceptions;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public class AppointmentBookingValidator : IAppointmentBookingValidator
    {
        private readonly IEmployeeRepository _employeeRepository;

        public AppointmentBookingValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> ValidateAsync(Companies company, DateTime scheduledAt, int estimatedDuration, CancellationToken cancellationToken)
        {
            var settings = company.Settings
                ?? throw new BadRequestException("Company booking settings not configured.");

            ValidateBookingDate(scheduledAt, settings);

            var workingHours = ResolveWorkingHours(company, scheduledAt);

            var appointmentEnd = scheduledAt.AddMinutes(estimatedDuration);

            ValidateWorkingHours(scheduledAt, appointmentEnd, workingHours);

            ValidateBookingInterval(scheduledAt, workingHours.OpenAt, settings.BookingIntervalMinutes);

            var employeeId = await ValidateAppointmentAvailabilityAsync(
                 company.Id,
                 scheduledAt,
                 appointmentEnd,
                 cancellationToken);

            return employeeId;
        }

        private static void ValidateBookingDate(DateTime scheduledAt, CompanySettings settings)
        {
            var currentDateTime = DateTime.Now;

            if (scheduledAt <= currentDateTime)
                throw new BadRequestException("Scheduled date and time must be in the future.");

            if (!settings.AllowSameDayBooking && scheduledAt.Date == currentDateTime.Date)
                throw new BadRequestException("Same day booking is not allowed.");

            var minimumNotice = TimeSpan.FromHours(settings.MinimumBookingNoticeHours);

            if (scheduledAt < currentDateTime.Add(minimumNotice))
                throw new BadRequestException($"Bookings must be made at least " + $"{settings.MinimumBookingNoticeHours} hours in advance.");

            if (settings.MaximumBookingDaysAhead > 0 && scheduledAt > currentDateTime.AddDays(settings.MaximumBookingDaysAhead))
                throw new BadRequestException($"Bookings cannot be made more than " + $"{settings.MaximumBookingDaysAhead} days ahead.");
        }

        private static BookingWorkingHours ResolveWorkingHours(Companies company, DateTime scheduledAt)
        {
            var date = scheduledAt.Date;

            var exception = company.CompanyWorkingHourExceptions?
                .FirstOrDefault(x => x.Date == DateOnly.FromDateTime(date));

            var baseHours = company.CompanyWorkingHours?
                .FirstOrDefault(x => x.DayOfWeek == scheduledAt.DayOfWeek);

            if (exception != null)
            {
                if (exception.IsClosed)
                    throw new BadRequestException("Company is closed on the requested date.");

                if (exception.OpenAt == null || exception.CloseAt == null)
                    throw new BadRequestException("Company working hours for requested date are not configured.");

                return new BookingWorkingHours(
                    date + exception.OpenAt.Value.ToTimeSpan(),
                    date + exception.CloseAt.Value.ToTimeSpan());
            }

            if (baseHours == null)
                throw new BadRequestException("Company working hours for requested day are not configured.");

            if (baseHours.IsClosed)
                throw new BadRequestException("Company is closed on the requested day of week.");

            return new BookingWorkingHours(
                date + baseHours.OpenAt.ToTimeSpan(),
                date + baseHours.CloseAt.ToTimeSpan());
        }

        private static void ValidateWorkingHours(DateTime scheduledAt, DateTime appointmentEnd, BookingWorkingHours workingHours)
        {
            if (scheduledAt < workingHours.OpenAt || appointmentEnd > workingHours.CloseAt)
                throw new BadRequestException($"Requested time is outside company working hours " + $"({workingHours.OpenAt:HH:mm} - {workingHours.CloseAt:HH:mm}).");
        }

        private static void ValidateBookingInterval(DateTime scheduledAt, DateTime openAt, int bookingIntervalMinutes)
        {
            if (bookingIntervalMinutes <= 0)
                return;

            var minutesFromOpen = (int)(scheduledAt - openAt).TotalMinutes;

            if (minutesFromOpen % bookingIntervalMinutes != 0)
                throw new BadRequestException($"Requested time must align with booking interval of " + $"{bookingIntervalMinutes} minutes.");
        }

        private async Task<int> ValidateAppointmentAvailabilityAsync(int companyId, DateTime scheduledAt, DateTime appointmentEnd, CancellationToken cancellationToken)
        {
            var employeeId = await _employeeRepository.FindAvailableEmployeeAsync(
                companyId,
                scheduledAt,
                appointmentEnd,
                cancellationToken);

            if (employeeId == null || employeeId == 0)
                throw new BadRequestException("No available employees for the requested time.");

            return employeeId.Value;
        }

        private sealed record BookingWorkingHours(DateTime OpenAt, DateTime CloseAt);
    }
}
