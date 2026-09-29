using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Persistence.Repositories
{
    public class ServiceTypeRepository : EfRepository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
