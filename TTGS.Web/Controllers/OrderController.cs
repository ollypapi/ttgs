using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TTGS.Core.Commands;
using TTGS.Core.Queries;
using TTGS.Shared.Constants;
using TTGS.Shared.DTOs;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;
using TTGS.Web.Security;

namespace TTGS.Web.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CustomAuthorization(UserRoleConstants.Admin)]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getstats")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> GetStats()
        {
            var orderStats = await _mediator.Send(new OrderStatusStatsQuery());
            return Ok(orderStats);
        }

        [HttpGet("getids")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> GetOrderIds([FromQuery] GetOrderIdsByStatusRequest orderIdsParameters)
        {
            var orderIds = await _mediator.Send(new GetOrderIdsQuery { Parameters = orderIdsParameters });

            HttpHeaderHelper.AddPagingMetaHeader(Response, orderIds);
            return Ok(orderIds);
        }

        [HttpPost("create")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            var isCreated = await _mediator.Send(new CreateOrderCommand { Order = createOrderRequest, Commit = true });
            return Ok(isCreated);
        }

        [HttpPut("update")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderStatusRequest updateOrderStatus)
        {
            var isUpdated = await _mediator.Send(new UpdateOrderStatusCommand { UpdateOrderStatus = updateOrderStatus});
            return Ok(isUpdated);
        }
    }
}
