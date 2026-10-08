using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class InspectionCategories
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Company { get; set; }
        public int CompanyId { get; set; }

        public ICollection<InspectionItemDefinition> InspectionItemDefinition { get; set; } = new List<InspectionItemDefinition>();
    }
}
