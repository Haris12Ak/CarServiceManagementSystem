using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Suppliers>> GetAllSuppliersTypeAsync(CancellationToken cancellationToken);
        Task<Suppliers> GetSupplierByIdAsync(int id, CancellationToken cancellationToken);
        Task<Suppliers> AddSupplierAsync(SupplierRequest request, CancellationToken cancellationToken);
        Task<Suppliers> UpdateSupplierAsync(int id, SupplierRequest request, CancellationToken cancellationToken);
    }
}
