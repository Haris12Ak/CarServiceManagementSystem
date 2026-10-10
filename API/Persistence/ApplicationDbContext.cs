using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryTransactions>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<InventoryTransactions>()
                .Property(x => x.ReferenceType)
                .HasConversion<string>();

            modelBuilder.Entity<Companies>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            modelBuilder.Entity<AppointmentServices>()
                .HasIndex(x => new
                {
                    x.AppointmentId,
                    x.ServiceTypeId
                }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<CompanySettings> CompanySettings { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<InspectionItems> InspectionItems { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryTransactions> InventoryTransactions { get; set; }
        public DbSet<InvoiceItems> InvoiceItems { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }
        public DbSet<SpareParts> SpareParts { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<VehicleInspections> VehicleInspections { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<WorkOrderParts> WorkOrderParts { get; set; }
        public DbSet<WorkOrders> WorkOrders { get; set; }
        public DbSet<WorkOrderServices> WorkOrderServices { get; set; }
        public DbSet<WorkOrderStatusHistories> WorkOrderStatusHistories { get; set; }
        public DbSet<InspectionItemDefinition> InspectionItemDefinition { get; set; }
        public DbSet<InspectionCategories> InspectionCategories { get; set; }
        public DbSet<CompanyWorkingHours> CompanyWorkingHours { get; set; }
        public DbSet<CompanyWorkingHourExceptions> CompanyWorkingHourExceptions { get; set; }
        public DbSet<AppointmentServices> AppointmentServices { get; set; }
    }
}
