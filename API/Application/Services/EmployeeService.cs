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
        private readonly ICurrentSystemUserService _currentSystemUserService;

        public EmployeeService(IEmployeeRepository employeeRepository, ICurrentSystemUserService currentSystemUserService)
        {
            _employeeRepository = employeeRepository;
            _currentSystemUserService = currentSystemUserService;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var employees = await _employeeRepository.GetAllAsync(companyId, cancellationToken);

            return employees.ToDomain();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
        {
            var companyId = await _currentSystemUserService.GetCompanyIdAsync(cancellationToken);

            var employee = await _employeeRepository.FindByIdAsync(id, companyId, cancellationToken)
                ?? throw new NotFoundException($"Employee with id {id} not found.");

            return employee.ToDomain();
        }
    }
}
