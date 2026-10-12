using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class AppointmentDtoMapper
    {
        public static Appointments ToDomain(this PublicAppointmentRequest request)
        {
            return new Appointments
            {
                ScheduledAt = request.ScheduledAt,
                Reason = request.Reason,
                Notes = request.Notes,
                CreatedAt = DateTime.Now
            };
        }

        public static Appointments ToDomain(this AppointmentRequest request)
        {
            return new Appointments
            {
                ClientId = request.ClientId,
                VehicleId = request.VehicleId,
                EmployeeId = request.EmployeeId,
                ScheduledAt = request.ScheduledAt,
                Reason = request.Reason,
                Notes = request.Notes,
                CreatedAt = DateTime.Now
            };
        }

        public static Client ToDomain(this PublicClientRequest request)
        {
            return new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                Notes = request.Notes,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static Vehicles ToDomain(this PublicVehicleRequest request)
        {
            return new Vehicles
            {
                VIN = request.VIN,
                LicensePlate = request.LicensePlate,
                Make = request.Make,
                Model = request.Model,
                Year = request.Year,
                Engine = request.Engine,
                FuelType = request.FuelType,
                Mileage = request.Mileage,
                Color = request.Color,
                Notes = request.Notes,
                CreatedAt = DateTime.Now
            };
        }
    }
}
