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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentSystemUserService _currentSystemUserService;
        private string CurrentUserId => _currentSystemUserService.KeycloakUserId;

        public UserController(IUserService userService, ICurrentSystemUserService currentSystemUserService)
        {
            _userService = userService;
            _currentSystemUserService = currentSystemUserService;
        }

        [HttpGet("Info")]
        public async Task<UserDto> GetUserInfo()
        {
            var userInfo = await _userService.GetUserInfoAsync(CurrentUserId);

            return userInfo.ToDto();
        }
    }
}
