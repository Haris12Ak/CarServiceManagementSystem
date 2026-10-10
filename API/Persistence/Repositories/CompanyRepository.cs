using Domain.Defaults;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CrateCompanyWithAdminAsync(Companies company, Employee adminEmployee, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                if (company.Settings == null)
                {
                    var domainDefaultsSettings = DefaultCompanySettings.Create();

                    company.Settings = new CompanySettings
                    {
                        Currency = domainDefaultsSettings.Currency,
                        TaxRate = domainDefaultsSettings.TaxRate,
                        InvoicePrefix = domainDefaultsSettings.InvoicePrefix,
                        WorkOrderPrefix = domainDefaultsSettings.WorkOrderPrefix,
                        DefaultLaborRate = domainDefaultsSettings.DefaultLaborRate,
                        CreatedAt = DateTime.Now,
                        BookingIntervalMinutes = domainDefaultsSettings.BookingIntervalMinutes,
                        MinimumBookingNoticeHours = domainDefaultsSettings.MinimumBookingNoticeHours,
                        MaximumBookingDaysAhead = domainDefaultsSettings.MaximumBookingDaysAhead,
                        AllowSameDayBooking = domainDefaultsSettings.AllowSameDayBooking,
                        RequireAppointmentConfirmation = domainDefaultsSettings.RequireAppointmentConfirmation,
                        OnlineBookingEnabled = domainDefaultsSettings.OnlineBookingEnabled
                    };
                }

                company.Settings.Companies = company;
                adminEmployee.Companies = company;

                await _context.Companies.AddAsync(company, cancellationToken);
                await _context.Employee.AddAsync(adminEmployee, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Companies> FindByIdAsync(int companyId, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .AsNoTracking()
                .Where(x => x.Id == companyId && x.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);

            if (company == null)
                return null;

            return company;
        }

        public async Task<bool> IsUserInCompanyAsync(string keycloakUserId, int companyId, CancellationToken cancellationToken)
        {
            var isEmployee = await _context.Employee
                .AnyAsync(e =>
                e.KeycloakUserId == keycloakUserId &&
                e.CompanyId == companyId &&
                e.IsActive == true,
                cancellationToken);

            if (isEmployee)
                return true;

            return await _context.Client
                .AnyAsync(c =>
                c.KeycloakUserId == keycloakUserId &&
                c.CompanyId == companyId &&
                c.IsActive == true,
                cancellationToken);
        }

        public async Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken)
        {
            return await _context.Companies
                .AsNoTracking()
                .AnyAsync(c =>
                c.Slug == slug,
                cancellationToken);
        }
    }
}
