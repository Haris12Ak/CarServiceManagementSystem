using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicles>> GetAllVehiclesAsync(CancellationToken cancellationToken);
        Task<List<Vehicles>> GetVehiclesByClientIdAsync(int clientId, CancellationToken cancellationToken);
        Task<Vehicles> GetVehicleById(int id, CancellationToken cancellationToken);
        Task<Vehicles> AddVehicleAsync(VehicleRequest request, CancellationToken cancellationToken);
        Task<Vehicles> UpdateVehicleAsync(int id, VehicleRequest request, CancellationToken cancellationToken);
    }
}
