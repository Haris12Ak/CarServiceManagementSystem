using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Suppliers>> GetAllSuppliersTypeAsync();
        Task<Suppliers> GetSupplierByIdAsync(int id);
        Task<Suppliers> AddSupplierAsync(SupplierRequest request);
        Task<Suppliers> UpdateSupplierAsync(int id, SupplierRequest request);
    }
}
