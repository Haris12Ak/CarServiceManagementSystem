using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class PersonBaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
