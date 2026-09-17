using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Requests
{
    public class CompanyRegistration
    {
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyTaxNumber { get; set; }
        public byte[]? CompanyLogo { get; set; }

        public EmployeeCreateRequest AdminEmployee { get; set; }
    }
}
