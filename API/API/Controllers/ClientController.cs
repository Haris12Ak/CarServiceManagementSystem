using Application.Authorization;
using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
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
        private string CurrentUserId => _currentSystemUserService.KeycloakUserId;

        public ClientController(IClientService clientService, ICurrentSystemUserService currentSystemUserService)
        {
            _clientService = clientService;
            _currentSystemUserService = currentSystemUserService;
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
    }
}
