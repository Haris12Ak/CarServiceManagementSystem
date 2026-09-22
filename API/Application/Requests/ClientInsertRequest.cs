using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class ClientInsertRequest : PersonBaseRequest
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string? Notes { get; set; }
    }
}
