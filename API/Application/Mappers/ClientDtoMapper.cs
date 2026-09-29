using Application.DTOs;
using Application.Requests;
using Azure.Core;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Application.Mappers
{
    public static class ClientDtoMapper
    {
        public static Client ToDomain(this ClientInsertRequest request, string keycloakUserId, int companyId)
        {
            return new Client
            {
                KeycloakUserId = keycloakUserId,
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

        public static ClientDto ToDto(this Client domain)
        {
            return new ClientDto
            {
                Id = domain.Id,
                CompanyId = domain.CompanyId,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = domain.Email,
                Phone = domain.Phone,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt,
                Address = domain.Address,
                City = domain.City,
                Notes = domain?.Notes,
            };
        }

        public static List<ClientDto> ToDto(this IEnumerable<Client> domains)
        {
            return domains.Select(domain => domain.ToDto()).ToList();
        }
    }
}
