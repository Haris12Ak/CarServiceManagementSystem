using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Client : PersonBase
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string? Notes { get; set; }
    }
}
