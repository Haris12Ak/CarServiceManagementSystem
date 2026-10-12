using System;
using System.Collections.Generic;
using System.Text;
using DomainCompanyWorkingHours = Domain.Models.CompanyWorkingHours;
using EntityCompanyWorkingHours = Persistence.Entities.CompanyWorkingHours;

namespace Persistence.Mappers
{
    public static class CompanyWorkingHoursMapper
    {
        public static DomainCompanyWorkingHours ToDomain(this EntityCompanyWorkingHours entity)
        {
            return new DomainCompanyWorkingHours
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                DayOfWeek = entity.DayOfWeek,
                OpenAt = entity.OpenAt,
                CloseAt = entity.CloseAt,
                IsClosed = entity.IsClosed
            };
        }

        public static List<DomainCompanyWorkingHours> ToDomain(this IEnumerable<EntityCompanyWorkingHours> entities)
        {
            return entities.Select(x => x.ToDomain()).ToList();
        }
    }
}
