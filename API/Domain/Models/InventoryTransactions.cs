using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class InventoryTransactions
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SparePartId { get; set; }
        public int EmployeeId { get; set; }
        public InventoryTransactionType Type { get; set; }
        public int Quantity { get; set; }
        public InventoryTransactionReferenceType? ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
    }
}
