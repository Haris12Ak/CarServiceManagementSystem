using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class SparePartsRepository : EfRepository<SpareParts>, ISparePartsRepository
    {
        public SparePartsRepository(ApplicationDbContext context) : base(context) { }
    }
}
