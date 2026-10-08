using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class InspectionItems
    {
        [Key]
        public int Id { get; set; }
        public InspectionItemStatus Status { get; set; }
        public string? Notes { get; set; }

        [ForeignKey(nameof(VehicleInspectionId))]
        public VehicleInspections VehicleInspections { get; set; }
        public int VehicleInspectionId { get; set; }

        [ForeignKey(nameof(InspectionItemDefinitionId))]
        public InspectionItemDefinition InspectionItemDefinition { get; set; }
        public int InspectionItemDefinitionId { get; set; }
    }
}
