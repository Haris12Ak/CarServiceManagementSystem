using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeeAsync(string keycloakUserId);
        Task<Employee> GetEmployeeByIdAsync(int id, string keycloakUserId);
    }
}
