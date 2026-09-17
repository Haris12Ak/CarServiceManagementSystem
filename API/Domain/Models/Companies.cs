using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Companies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string TaxNumber { get; set; }
        public byte[]? Logo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public CompanySettings Settings { get; set; }
    }
}
