using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class SparePartsDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
