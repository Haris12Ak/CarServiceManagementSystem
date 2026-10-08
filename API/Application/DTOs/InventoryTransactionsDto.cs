using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class InventoryTransactionsDto
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
