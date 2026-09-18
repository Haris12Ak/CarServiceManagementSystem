using Application.Interfaces;
using Application.Requests;
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

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
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
    }
}
