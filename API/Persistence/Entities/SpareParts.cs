using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class SpareParts
    {
        [Key]
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Suppliers Suppliers { get; set; }
        public int SupplierId { get; set; }

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<InventoryTransactions> InventoryTransactions { get; set; } = new List<InventoryTransactions>();
        public ICollection<WorkOrderParts> WorkOrderParts { get; set; } = new List<WorkOrderParts>();

    }
}
