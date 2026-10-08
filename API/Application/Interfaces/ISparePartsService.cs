using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISparePartsService
    {
        Task<List<SpareParts>> GetAllSparePartsAsync(CancellationToken cancellationToken);
        Task<SpareParts> GetSparePartsByIdAsync(int id, CancellationToken cancellationToken);
        Task<SpareParts> AddSparePartsAsync(SparePartsRequest request, CancellationToken cancellationToken);
        Task<SpareParts> UpdateSparePartsAsync(int id, SparePartsRequest request, CancellationToken cancellationToken);
    }
}
