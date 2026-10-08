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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Info")]
        public async Task<UserDto> GetUserInfo(CancellationToken cancellationToken)
        {
            var userInfo = await _userService.GetUserInfoAsync(cancellationToken);

            return userInfo.ToDto();
        }
    }
}
