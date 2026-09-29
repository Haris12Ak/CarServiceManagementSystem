using Application.Authorization;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyAuthorizationService _companyAuthorizationService;

        public EmployeeService(IEmployeeRepository employeeRepository, ICompanyAuthorizationService companyAuthorizationService)
        {
            _employeeRepository = employeeRepository;
            _companyAuthorizationService = companyAuthorizationService;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync(string keycloakUserId)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var employees = await _employeeRepository.GetAllAsync(companyId);

            return employees.ToDomain();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id, string keycloakUserId)
        {
            var companyId = await _companyAuthorizationService.GetCurrentUserCompanyIdAsync(keycloakUserId);

            var employee = await _employeeRepository.FindByIdAsync(id, companyId);

            if (employee == null)
                throw new NotFoundException($"Employee with id {id} not found.");

            return employee.ToDomain();
        }
    }
}
