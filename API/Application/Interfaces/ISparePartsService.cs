using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISparePartsService
    {
        Task<List<SpareParts>> GetAllSparePartsAsync();
        Task<SpareParts> GetSparePartsByIdAsync(int id);
        Task<SpareParts> AddSparePartsAsync(SparePartsRequest request);
        Task<SpareParts> UpdateSparePartsAsync(int id, SparePartsRequest request);
    }
}
