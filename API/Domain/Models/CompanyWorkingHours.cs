using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class CompanyWorkingHours
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly OpenAt { get; set; }
        public TimeOnly CloseAt { get; set; }
        public bool IsClosed { get; set; }
    }
}
