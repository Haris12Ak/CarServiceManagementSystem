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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;

        public EmployeeController(
            IEmployeeService employeeService,
            ICompanyService companyService)
        {
            _employeeService = employeeService;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<List<EmployeeDto>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeeAsync();
            return employees.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDto> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return employee.ToDto();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToCompany([FromBody] EmployeeInsertRequest request)
        {
            try
            {
                await _companyService.AddEmployeeToCompanyAsync(request);

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

    }
}
