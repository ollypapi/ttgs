using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;
using TTGS.Shared.Response;

namespace TTGS.Core.Queries
{
    public class GetOrderIdsQuery : IRequest<PagedList<OrderIdsResponse>>
    {
        public GetOrderIdsByStatusRequest Parameters { get; set; }
    }

    public class GetOrderIdsQueryHandler : IRequestHandler<GetOrderIdsQuery, PagedList<OrderIdsResponse>>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;

        public GetOrderIdsQueryHandler(ITTGSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<OrderIdsResponse>> Handle(GetOrderIdsQuery request, CancellationToken cancellationToken)
        {
          return await Task.Run(() => PagedList<OrderIdsResponse>.ToPagedList(
               _unitOfWork.Orders.AsQueryable()
               .Where(x => x.OrderStatus == request.Parameters.OrderStatus.ToString())
               .Select(x => new OrderIdsResponse { Id = x.Id, DateCreated = x.DateCreated})
               .OrderByDescending(x => x.DateCreated),
               request.Parameters.PageNumber,
               request.Parameters.PageSize));
        }
    }
}
