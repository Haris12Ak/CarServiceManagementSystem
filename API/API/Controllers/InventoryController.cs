using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Mappers;

using Application.Requests;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "owner")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("{sparePartId}")]
        public async Task<InventoryDto> GetInventoryBySparePartId(int sparePartId)
        {
            var inventory = await _inventoryService.GetInventoryBySparePartIdAsync(sparePartId);

            return inventory.ToDto();
        }

        [HttpGet("{sparePartId}/transactions")]
        public async Task<List<InventoryTransactionsDto>> GetInventoryTransaction(int sparePartId)
        {
            var transactions = await _inventoryService.GetInventoryTransactionAsync(sparePartId);

            return transactions.ToDto();
        }

        [HttpPost("{sparePartId}/add")]
        public async Task<InventoryDto> AddStock(int sparePartId, [FromBody] InventoryRequest request)
        {
            var inventory = await _inventoryService.AddStockAsync(sparePartId, request);

            return inventory.ToDto();
        }

        [HttpPost("{sparePartId}/remove")]
        public async Task<InventoryDto> RemoveStock(int sparePartId, [FromQuery] int workOrderId, [FromBody] InventoryRequest request)
        {
            var inventory = await _inventoryService.RemoveStockAsync(sparePartId, workOrderId, request);

            return inventory.ToDto();
        }

        [HttpPost("{sparePartId}/adjust-in")]
        public async Task<InventoryDto> AdjustIn(int sparePartId, [FromBody] InventoryRequest request)
        {
            var inventory = await _inventoryService.AdjustInAsync(sparePartId, request);

            return inventory.ToDto();
        }

        [HttpPost("{sparePartId}/adjust-out")]
        public async Task<InventoryDto> AdjustOut(int sparePartId, [FromBody] InventoryRequest request)
        {
            var inventory = await _inventoryService.AdjustOutAsync(sparePartId, request);

            return inventory.ToDto();
        }
    }
}
