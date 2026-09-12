using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistence.Entities
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string TaxNumber { get; set; }
        public byte[]? Logo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public CompanySettings Settings { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<Vehicles> Vehicles { get; set; } = new List<Vehicles>();
        public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
        public ICollection<Services> Services { get; set; } = new List<Services>();
        public ICollection<WorkOrders> WorkOrders { get; set; } = new List<WorkOrders>();
        public ICollection<Suppliers> Suppliers { get; set; } = new List<Suppliers>();
        public ICollection<SpareParts> SpareParts { get; set; } = new List<SpareParts>();
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<InventoryTransactions> InventoryTransactions { get; set; } = new List<InventoryTransactions>();
        public ICollection<VehicleInspections> VehicleInspections { get; set; } = new List<VehicleInspections>();
        public ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
    }
}
