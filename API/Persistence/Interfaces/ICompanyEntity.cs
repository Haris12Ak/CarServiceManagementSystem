using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces
{
    public interface ICompanyEntity : IEntity
    {
        int CompanyId { get; set; }
    }
}
