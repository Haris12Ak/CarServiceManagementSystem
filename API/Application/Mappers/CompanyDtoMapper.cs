using Application.Requests;
using Domain.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class CompanyDtoMapper
    {
        public static Companies ToDomain(this CompanyRegistration request, string slug)
        {
            return new Companies
            {
                Name = request.CompanyName,
                Slug = slug,
                Email = request.CompanyEmail,
                Phone = request.CompanyPhone,
                Address = request.CompanyAddress,
                City = request.CompanyCity,
                TaxNumber = request.CompanyTaxNumber,
                Logo = request.CompanyLogo,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }
    }
}
