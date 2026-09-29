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
        private readonly ICurrentSystemUserService _currentSystemUserService;
        private readonly ICompanyService _companyService;
        private string CurrentUserId => _currentSystemUserService.KeycloakUserId;

        public ClientController(
            IClientService clientService,
            ICurrentSystemUserService currentSystemUserService,
            ICompanyService companyService)
        {
            _clientService = clientService;
            _currentSystemUserService = currentSystemUserService;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<List<ClientDto>> GetAll()
        {
            var clients = await _clientService.GetAllClientAsync(CurrentUserId);

            return clients.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> GetById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id, CurrentUserId);

            return client.ToDto();
        }

        [HttpPost]
        public async Task<IActionResult> AddClientToCompany([FromBody] ClientInsertRequest request)
        {
            try
            {
                await _companyService.AddClientToCompanyAsync(CurrentUserId, request);

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
