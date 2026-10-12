using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class CompanyWorkingHourExceptions
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateOnly Date { get; set; }
        public bool IsClosed { get; set; }
        public TimeOnly? OpenAt { get; set; }
        public TimeOnly? CloseAt { get; set; }
        public string? Reason { get; set; }
    }
}
