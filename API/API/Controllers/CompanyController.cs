using Application.Authorization;
using Application.Interfaces;
using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public CompanyController(
            ICompanyService companyService,
            ICurrentSystemUserService currentSystemUserService)
        {
            _companyService = companyService;
            _currentSystemUserService = currentSystemUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCompany([FromBody] CompanyRegistration registration)
        {
            try
            {
                await _companyService.RegisterCompanyAsync(registration);

                return Ok(new { Message = "You have successfully registered your company." });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Company registration failed.",
                    Error = ex.Message
                });
            }
        }

        [Authorize(Roles = "owner")]
        [HttpPost("AddEmployeeToCompany")]
        public async Task<IActionResult> AddEmployeeToCompany(EmployeeInsertRequest request)
        {
            try
            {
                var currentUser = _currentSystemUserService.KeycloakUserId;

                await _companyService.AddEmployeeToCompanyAsync(currentUser, request);

                return Ok(new { Message = $"Employee {request.FirstName} {request.LastName} has been successfully added." });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Adding new employee failed.",
                    Error = ex.Message
                });
            }
        }

        [Authorize(Roles = "owner")]
        [HttpPost("AddClientToCompany")]
        public async Task<IActionResult> AddClientToCompany(ClientInsertRequest request)
        {
            try
            {
                var currentUser = _currentSystemUserService.KeycloakUserId;

                await _companyService.AddClientToCompanyAsync(currentUser, request);

                return Ok(new { Message = $"Client {request.FirstName} {request.LastName} has been successfully added." });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Adding new employee failed.",
                    Error = ex.Message
                });
            }
        }
    }
}
