using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SparePartId { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }
        public SpareParts SpareParts { get; set; }
    }
}
