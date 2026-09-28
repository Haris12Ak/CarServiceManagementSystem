using Application.Requests;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
