using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class InventoryRequest
    {
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
