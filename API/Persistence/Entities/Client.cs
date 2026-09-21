using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Client : PersonBase
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string? Notes { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; } = new List<Vehicles>();
        public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
        public ICollection<WorkOrders> WorkOrders { get; set; } = new List<WorkOrders>();
        public ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
    }
}
