using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class UserDto
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? Position { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Notes { get; set; }

        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyCity { get; set; }
        public string? CompanyTaxNumber { get; set; }
    }
}
