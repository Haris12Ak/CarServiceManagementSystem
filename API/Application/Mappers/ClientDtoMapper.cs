using Application.Requests;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Application.Mappers
{
    public static class ClientDtoMapper
    {
        public static Client ToDomain(this ClientInsertRequest request, string keylcoakUserId, int companyId)
        {
            return new Client
            {
                KeycloakUserId = keylcoakUserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                IsActive = true,
                CreatedAt = DateTime.Now,
                Address = request.Address,
                City = request.City,
                Notes = request?.Notes,
                CompanyId = companyId
            };
        }
    }
}
