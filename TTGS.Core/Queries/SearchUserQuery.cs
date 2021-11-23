using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;
using TTGS.Shared.Response;

namespace TTGS.Core.Queries
{
    public class SearchUserQuery : IRequest<PagedList<SearchUserResponse>>
    {
        public SearchUserRequest Parameters { get; set; }
    }
    public class SearchUserQueryHandler : IRequestHandler<SearchUserQuery, PagedList<SearchUserResponse>>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;

        public SearchUserQueryHandler(ITTGSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<SearchUserResponse>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => PagedList<SearchUserResponse>.ToPagedList(
               _unitOfWork.AspNetUsers.AsQueryable()
               .Where(x => x.FirstName.Contains(request.Parameters.searchParams) || x.LastName.Contains(request.Parameters.searchParams)
                    || x.Email.Contains(request.Parameters.searchParams))
               .Select(x => new SearchUserResponse {FirstName = x.FirstName,LastName = x.LastName,UserName = x.UserName,Email = x.Email})
               .OrderBy(x=>x.FirstName),
               request.Parameters.PageNumber,
               request.Parameters.PageSize));
        }
    }
}
