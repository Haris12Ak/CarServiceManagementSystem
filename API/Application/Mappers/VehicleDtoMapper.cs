using Application.DTOs;
using Application.Requests;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Application.Mappers
{
    public static class VehicleDtoMapper
    {
        public static VehicleDto ToDto(this Vehicles domain)
        {
            return new VehicleDto
            {
                Id = domain.Id,
                CompanyId = domain.CompanyId,
                ClientId = domain.ClientId,
                VIN = domain.VIN,
                LicensePlate = domain.LicensePlate,
                Make = domain.Make,
                Model = domain.Model,
                Year = domain.Year,
                Engine = domain.Engine,
                FuelType = domain.FuelType,
                Mileage = domain.Mileage,
                Color = domain.Color,
                Notes = domain.Notes,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt
            };
        }

        public static List<VehicleDto> ToDto(this IEnumerable<Vehicles> domains)
        {
            return domains.Select(domain => domain.ToDto()).ToList();
        }

        public static Vehicles ToDomain(this VehicleRequest request, int clientId, int companyId)
        {
            return new Vehicles
            {
                CompanyId = companyId,
                ClientId = clientId,
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

        public static void ApplyTo(this VehicleRequest request, Vehicles domain)
        {
            domain.CompanyId = request.ClientId;
            domain.VIN = request.VIN;
            domain.LicensePlate = request.LicensePlate;
            domain.Make = request.Make;
            domain.Model = request.Model;
            domain.Year = request.Year;
            domain.Engine = request.Engine;
            domain.FuelType = request.FuelType;
            domain.Mileage = request.Mileage;
            domain.Color = request.Color;
            domain.Notes = request.Notes;
        }
    }
}
