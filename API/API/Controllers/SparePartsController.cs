using Application.DTOs;
using Application.Interfaces;
using Application.Requests;
using Application.Mappers;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "owner")]
    public class SparePartsController : ControllerBase
    {
        private readonly ISparePartsService _sparePartsService;

        public SparePartsController(ISparePartsService sparePartsService)
        {
            _sparePartsService = sparePartsService;
        }

        [HttpGet]
        public async Task<List<SparePartsDto>> GetAll()
        {
            var spareParts = await _sparePartsService.GetAllSparePartsAsync();

            return spareParts.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<SparePartsDto> GetById(int id)
        {
            var spareParts = await _sparePartsService.GetSparePartsByIdAsync(id);

            return spareParts.ToDto();
        }

        [HttpPost]
        public async Task<SparePartsDto> Add([FromBody] SparePartsRequest request)
        {
            var spareParts = await _sparePartsService.AddSparePartsAsync(request);

            return spareParts.ToDto();
        }

        [HttpPut("{id}")]
        public async Task<SparePartsDto> Update(int id, [FromBody] SparePartsRequest request)
        {
            var spareParts = await _sparePartsService.UpdateSparePartsAsync(id, request);

            return spareParts.ToDto();
        }
    }
}
