using System;
using System.Collections.Generic;
using System.Text;
using DomainCompanyWorkingHourExceptions = Domain.Models.CompanyWorkingHourExceptions;
using EntityCompanyWorkingHourExceptions = Persistence.Entities.CompanyWorkingHourExceptions;

namespace Persistence.Mappers
{
    public static class CompanyWorkingHourExceptionsMapper
    {
        public static DomainCompanyWorkingHourExceptions ToDomain(this EntityCompanyWorkingHourExceptions entity)
        {
            return new DomainCompanyWorkingHourExceptions
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Date = entity.Date,
                IsClosed = entity.IsClosed,
                OpenAt = entity.OpenAt,
                CloseAt = entity.CloseAt,
                Reason = entity.Reason
            };
        }

        public static List<DomainCompanyWorkingHourExceptions> ToDomain(this IEnumerable<EntityCompanyWorkingHourExceptions> entities)
        {
            return entities.Select(x => x.ToDomain()).ToList();
        }
    }
}
