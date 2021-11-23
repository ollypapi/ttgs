using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.Entity;
using TTGS.Shared.Enums;

namespace TTGS.Shared.Request
{
    public class UpdateOrderStatusRequest
    {
        public long Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
