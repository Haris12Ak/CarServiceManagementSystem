using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class SparePartsRequest
    {
        public int SupplierId { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int MinimumStock { get; set; }
    }
}
