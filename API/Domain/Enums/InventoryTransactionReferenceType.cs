using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum InventoryTransactionReferenceType
    {
        InitialStock,
        Purchase,
        WorkOrder,
        Adjustment,
        Return
    }
}
