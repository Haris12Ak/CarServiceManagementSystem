using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? Notes { get; set; }
    }
}
