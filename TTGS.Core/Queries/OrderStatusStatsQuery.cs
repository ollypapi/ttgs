using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Enums;
using TTGS.Shared.Response;

namespace TTGS.Core.Queries
{
    public class OrderStatusStatsQuery : IRequest<OrderStatusStatsResponse>
    {
    }

    public class OrderStatusStatsQueryHandler : IRequestHandler<OrderStatusStatsQuery, OrderStatusStatsResponse>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;

        public OrderStatusStatsQueryHandler(ITTGSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderStatusStatsResponse> Handle(OrderStatusStatsQuery request, CancellationToken cancellationToken)
        {
            var response = await Task.Run(() =>
                _unitOfWork.Orders.AsQueryable().GroupBy(x => x.OrderStatus)
                .Select(x => new OrderStatusStatsResponse
                {
                    OrderReceived = x.Count(y => y.OrderStatus == OrderStatusEnum.Received.ToString()),
                    OrderLoaded = x.Count(y => y.OrderStatus == OrderStatusEnum.Loaded.ToString()),
                    OrderDelivered = x.Count(y => y.OrderStatus == OrderStatusEnum.Delivered.ToString()),
                    OrderInProgress = x.Count(y => y.OrderStatus == OrderStatusEnum.InProgress.ToString()),
                }).FirstOrDefault());

            return response;
        }
    }
}
