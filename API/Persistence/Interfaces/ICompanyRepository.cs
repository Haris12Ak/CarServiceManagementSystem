using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface ICompanyRepository
    {
        Task CrateCompanyWithAdminAsync(Companies company, Employee adminEmployee);
    }
}
