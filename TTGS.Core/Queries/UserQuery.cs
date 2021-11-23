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
    public class UserQuery : IRequest<ListUserResponse>
    {
        public string Id { get; set; }
    }

    public class UserQueryHandler : IRequestHandler<UserQuery, ListUserResponse>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUsers> _userManager;

        public UserQueryHandler(ITTGSUnitOfWork unitOfWork,
            UserManager<AspNetUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ListUserResponse> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var response = await Task.Run(() =>
                _unitOfWork.AspNetUsers.AsQueryable().Select(x => new ListUserResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CellPhone = x.PhoneNumber,
                    Email = x.Email,
                    User = x
                })
                .FirstOrDefault(x => x.Id == request.Id));

            if (response != null)
            {
                var roles = await _userManager.GetRolesAsync(response.User);
                response.Roles.AddRange(roles);
            }

            return response;
        }
    }
}
