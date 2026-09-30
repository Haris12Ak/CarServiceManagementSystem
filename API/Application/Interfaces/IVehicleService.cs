using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicles>> GetAllVehiclesAsync();
        Task<List<Vehicles>> GetVehiclesByClientIdAsync(int clientId);
        Task<Vehicles> GetVehicleById(int id);
        Task<Vehicles> AddVehicleAsync(VehicleRequest request);
        Task<Vehicles> UpdateVehicleAsync(int id, VehicleRequest request);
    }
}
