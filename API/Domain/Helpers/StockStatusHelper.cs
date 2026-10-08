using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public static class StockStatusHelper
    {
        public static StockStatus GetStockStatus(int quantity, int minimumStock)
        {
            if (quantity <= 0)
                return StockStatus.OutOfStock;

            if (quantity <= minimumStock)
                return StockStatus.Low;

            return StockStatus.Normal;
        }
    }
}
