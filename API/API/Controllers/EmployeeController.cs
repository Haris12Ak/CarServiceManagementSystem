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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICurrentSystemUserService _currentSystemUserService;

        private string CurrentUserId => _currentSystemUserService.KeycloakUserId;

        public EmployeeController(IEmployeeService employeeService, ICurrentSystemUserService currentSystemUserService)
        {
            _employeeService = employeeService;
            _currentSystemUserService = currentSystemUserService;
        }

        [HttpGet]
        public async Task<List<EmployeeDto>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeeAsync(CurrentUserId);
            return employees.ToDto();
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDto> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id, CurrentUserId);
            return employee.ToDto();
        }

    }
}
