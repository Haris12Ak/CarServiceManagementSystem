using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(SparePartId))]
        public SpareParts SpareParts { get; set; }
        public int SparePartId { get; set; }
    }

}
