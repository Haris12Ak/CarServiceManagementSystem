using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "owner")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<List<SupplierDto>> GetAll(CancellationToken cancellationToken)
        {
            var suppliers = await _supplierService.GetAllSuppliersTypeAsync(cancellationToken);

            return suppliers.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<SupplierDto> GetById(int id, CancellationToken cancellationToken)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id, cancellationToken);

            return supplier.ToDto();
        }

        [HttpPost]
        public async Task<SupplierDto> Add([FromBody] SupplierRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierService.AddSupplierAsync(request, cancellationToken);

            return supplier.ToDto();
        }

        [HttpPut("{id}")]
        public async Task<SupplierDto> Update(int id, [FromBody] SupplierRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierService.UpdateSupplierAsync(id, request, cancellationToken);

            return supplier.ToDto();
        }
    }
}
