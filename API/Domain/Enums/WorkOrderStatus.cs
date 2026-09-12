using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum WorkOrderStatus
    {
        Draft,
        WaitingForApproval,
        Approved,
        InProgress,
        WaitingForParts,
        Completed,
        Cancelled
    }
}
