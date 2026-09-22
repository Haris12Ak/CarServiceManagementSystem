using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class EmployeeInsertRequest : PersonBaseRequest
    {
        public string Position { get; set; }
    }
}
