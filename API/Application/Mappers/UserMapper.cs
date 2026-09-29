using Application.DTOs;
using Application.Requests;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static User MapToUser(string firstName, string lastName, string email, string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                username = $"{firstName.ToLower()}_{lastName.ToLower()}";
                password = PasswordGenerator.GeneratePassword();
            }

            return new User
            {
                Username = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };
        }

        public static UserDto ToDto(
            this (Employee? employee, Client? client, Companies? company) userInfo)
        {
            var (employee, client, company) = userInfo;

            var dto = new UserDto();

            // Company information
            if (company != null)
            {
                dto.CompanyId = company.Id;
                dto.CompanyName = company.Name;
                dto.CompanyEmail = company.Email;
                dto.CompanyPhone = company.Phone;
                dto.CompanyAddress = company.Address;
                dto.CompanyCity = company.City;
                dto.CompanyTaxNumber = company.TaxNumber;
            }

            // Employee information
            if (employee != null)
            {
                dto.UserId = employee.Id;
                dto.FirstName = employee.FirstName;
                dto.LastName = employee.LastName;
                dto.Email = employee.Email;
                dto.Phone = employee.Phone;
                dto.CreatedAt = employee.CreatedAt;
                dto.Position = employee.Position;

                return dto;
            }

            // Client information
            if (client != null)
            {
                dto.UserId = client.Id;
                dto.FirstName = client.FirstName;
                dto.LastName = client.LastName;
                dto.Email = client.Email;
                dto.Phone = client.Phone;
                dto.CreatedAt = client.CreatedAt;
                dto.Address = client.Address;
                dto.City = client.City;
                dto.Notes = client.Notes;
            }

            return dto;
        }
    }
}
