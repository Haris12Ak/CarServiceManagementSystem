using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    public class AppointmentServices
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int EstimatedDuration { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        public Appointments Appointments { get; set; }
        public int AppointmentId { get; set; }

        [ForeignKey(nameof(ServiceTypeId))]
        public ServiceType ServiceType { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
