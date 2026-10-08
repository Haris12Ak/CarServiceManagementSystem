using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class InspectionItemDefinition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }
        public int CompanyId { get; set; }


        [ForeignKey(nameof(InspectionCategoryId))]
        public InspectionCategories InspectionCategories { get; set; }
        public int InspectionCategoryId { get; set; }

        public ICollection<InspectionItems> InspectionItems { get; set; } = new List<InspectionItems>();
    }
}
