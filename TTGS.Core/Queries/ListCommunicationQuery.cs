using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.DTOs;
using TTGS.Shared.Helper;
using TTGS.Shared.Response;

namespace TTGS.Core.Queries
{
    public class ListCommunicationQuery : IRequest<PagedList<ListCommunicationResponse>>
    {
        public Guid UserId { get; set; }
        public PagingParameters Parameters { get; set; }
    }

    public class ListCommunicationQueryHandler : IRequestHandler<ListCommunicationQuery, PagedList<ListCommunicationResponse>>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;

        public ListCommunicationQueryHandler(ITTGSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<ListCommunicationResponse>> Handle(ListCommunicationQuery request, CancellationToken cancellationToken)
        {
            var response = await Task.Run(() => PagedList<ListCommunicationResponse>.ToPagedList(
                _unitOfWork.Communications.AsQueryable()
                .Where(x => x.UserId == request.UserId || x.RecipientId == request.UserId)
                .Select(x => new ListCommunicationResponse
                {
                    DateCreated = x.DateCreated,
                    IsCreatedByAdmin = x.IsCreatedByAdmin, 
                    Subject = x.Subject,
                    Message = x.Message
                })
                .OrderByDescending(x => x.DateCreated), 
                request.Parameters.PageNumber,
                request.Parameters.PageSize));

            return response;
        }
    }
}
