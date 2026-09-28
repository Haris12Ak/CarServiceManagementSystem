using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Application.Mappers
{
    public static class EmployeeDtoMapper
    {
        public static Employee ToDomain(
            this CompanyRegistration request,
            string keycloakUserId)
        {
            return new Employee
            {
                KeycloakUserId = keycloakUserId,
                FirstName = request.AdminEmployee.FirstName,
                LastName = request.AdminEmployee.LastName,
                Email = request.AdminEmployee.Email,
                Phone = request.AdminEmployee.Phone,
                Position = request.AdminEmployee.Position,
                IsActive = true,
                CreatedAt = DateTime.Now,
            };
        }

        public static Employee ToDomain(
            this EmployeeInsertRequest request,
            string keycloakUserId,
            int companyId)
        {
            return new Employee
            {
                KeycloakUserId = keycloakUserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                IsActive = true,
                CreatedAt = DateTime.Now,
                Position = request.Position,
                CompanyId = companyId
            };
        }
    }
}
