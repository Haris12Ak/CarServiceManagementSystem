using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class SupplierRequest
    {
        public string Name { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? TaxNumber { get; set; }
    }
}
