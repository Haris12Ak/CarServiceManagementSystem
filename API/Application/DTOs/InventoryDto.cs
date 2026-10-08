using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SparePartId { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
