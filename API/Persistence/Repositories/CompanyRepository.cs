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

        public async Task CrateCompanyWithAdminAsync(Companies company, Employee adminEmployee)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

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
                        AppointmentDuration = domainDefaultsSettings.AppointmentDuration,
                        CreatedAt = DateTime.Now
                    };
                }

                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();

                adminEmployee.CompanyId = company.Id;
                await _context.Employee.AddAsync(adminEmployee);
                await _context.SaveChangesAsync();

                if (company.Settings != null)
                {
                    company.Settings.CompanyId = company.Id;
                    _context.CompanySettings.Update(company.Settings);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Companies> FindByIdAsync(int companyId)
        {
            var company = await _context.Companies
                .FindAsync(companyId);

            if (company == null)
                return null;

            return company;
        }
    }
}
