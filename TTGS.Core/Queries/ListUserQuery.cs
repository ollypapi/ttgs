using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.DTOs;
using TTGS.Shared.Helper;
using TTGS.Shared.Response;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TTGS.Shared.Entity;

namespace TTGS.Core.Queries
{
    public class ListUserQuery : IRequest<PagedList<ListUserResponse>>
    {
        public PagingParameters Parameters { get; set; }
    }

    public class ListUserQueryHandler : IRequestHandler<ListUserQuery, PagedList<ListUserResponse>>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUsers> _userManager;

        public ListUserQueryHandler(ITTGSUnitOfWork unitOfWork,
            UserManager<AspNetUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<PagedList<ListUserResponse>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            var response = await Task.Run(() => PagedList<ListUserResponse>.ToPagedList(
                _unitOfWork.AspNetUsers.AsQueryable()
                .Select(x => new ListUserResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CellPhone = x.PhoneNumber,
                    Email = x.Email,
                    User = x
                })
                .OrderBy(x => x.FirstName), 
                request.Parameters.PageNumber,
                request.Parameters.PageSize));

            response.ForEach(x => 
            {
                var roles = _userManager.GetRolesAsync(x.User).GetAwaiter().GetResult();
                x.Roles.AddRange(roles); 
            });

            return response;
        }
    }
}
