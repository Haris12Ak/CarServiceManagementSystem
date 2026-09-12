using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class InventoryTransactions
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string? ReferenceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(SparePartId))]
        public SpareParts SpareParts { get; set; }
        public int SparePartId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

    }
}
