using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class EmployeeInsertRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
    }
}
