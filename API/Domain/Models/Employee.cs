using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Employee : PersonBase
    {
        public string Position { get; set; }
    }
}
