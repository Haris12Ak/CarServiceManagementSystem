using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<(Employee? emoloyee, Client? client, Companies? company)> GetUserInfoAsync();
    }
}
