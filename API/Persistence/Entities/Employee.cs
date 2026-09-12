using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entities
{
    public class Employee : PersonBase
    {
        public string Position { get; set; }

        public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
        public ICollection<WorkOrders> WorkOrders { get; set; } = new List<WorkOrders>();
        public ICollection<WorkOrderStatusHistories> WorkOrderStatusHistories { get; set; } = new List<WorkOrderStatusHistories>();
        public ICollection<InventoryTransactions> InventoryTransactions { get; set; } = new List<InventoryTransactions>();
        public ICollection<VehicleInspections> VehicleInspections { get; set; } = new List<VehicleInspections>();
    }
}