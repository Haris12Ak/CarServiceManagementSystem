using Application.Authorization;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ICompanyService _companyService;

        public ClientController(
            IClientService clientService,
            ICompanyService companyService)
        {
            _clientService = clientService;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<List<ClientDto>> GetAll(CancellationToken cancellationToken)
        {
            var clients = await _clientService.GetAllClientAsync(cancellationToken);

            return clients.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> GetById(int id, CancellationToken cancellationToken)
        {
            var client = await _clientService.GetClientByIdAsync(id, cancellationToken);

            return client.ToDto();
        }

        [HttpPost]
        public async Task<IActionResult> AddClientToCompany([FromBody] ClientInsertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _companyService.AddClientToCompanyAsync(request, cancellationToken);

                return Ok(new { Message = $"Client {request.FirstName} {request.LastName} has been successfully added." });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Adding new client failed.",
                    Error = ex.Message
                });
            }
        }
    }
}
