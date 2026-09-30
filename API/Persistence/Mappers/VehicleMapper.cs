using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static Azure.Core.HttpHeader;
using DomainVehicle = Domain.Models.Vehicles;
using EntityVehicle = Persistence.Entities.Vehicles;

namespace Persistence.Mappers
{
    public static class VehicleMapper
    {
        public static EntityVehicle ToEntity(this DomainVehicle domain)
        {
            return new EntityVehicle
            {
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

        public static DomainVehicle ToDomain(this EntityVehicle entity)
        {
            return new DomainVehicle
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                ClientId = entity.ClientId,
                VIN = entity.VIN,
                LicensePlate = entity.LicensePlate,
                Make = entity.Make,
                Model = entity.Model,
                Year = entity.Year,
                Engine = entity.Engine,
                FuelType = entity.FuelType,
                Mileage = entity.Mileage,
                Color = entity.Color,
                Notes = entity.Notes,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static List<DomainVehicle> ToDomain(this IEnumerable<EntityVehicle> entities)
        {
            return entities.Select(entity => entity.ToDomain()).ToList();
        }

        public static void ApplyTo(this DomainVehicle domain, EntityVehicle entity)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.VIN = domain.VIN;
            entity.LicensePlate = domain.LicensePlate;
            entity.Make = domain.Make;
            entity.Model = domain.Model;
            entity.Year = domain.Year;
            entity.Engine = domain.Engine;
            entity.FuelType = domain.FuelType;
            entity.Mileage = domain.Mileage;
            entity.Color = domain.Color;
            entity.Notes = domain.Notes;
            entity.UpdatedAt = DateTime.Now;
        }
    }
}
