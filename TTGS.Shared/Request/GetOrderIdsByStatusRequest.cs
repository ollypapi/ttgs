using TTGS.Shared.DTOs;
using TTGS.Shared.Enums;

namespace TTGS.Shared.Request
{
    public class GetOrderIdsByStatusRequest : PagingParameters
    {
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
