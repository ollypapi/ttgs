using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Constants;
using TTGS.Shared.Entity;
using TTGS.Shared.Request;

namespace TTGS.Core.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UpdateUserRequest User { get; set; }
        public bool Commit { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUsers> _userManager;

        public UpdateUserCommandHandler(ITTGSUnitOfWork unitOfWork,
            UserManager<AspNetUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.AspNetUsers.AsQueryable().FirstOrDefault(x => x.Id == request.User.Id);

            if (user != null)
            {
                user.FirstName = request.User.FirstName;
                user.LastName = request.User.LastName;
                user.PhoneNumber = request.User.CellPhone;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.User.Password);
            }
            _unitOfWork.AspNetUsers.Update(user);
            await _unitOfWork.SaveAsync();

            foreach (var role in request.User.Roles)
                if (UserRoleConstants.AllowedRoles.Contains(role) && !(await _userManager.IsInRoleAsync(user, role)))
                    await _userManager.AddToRoleAsync(user, role);

            foreach (var role in request.User.DisabledRoles)
                if (UserRoleConstants.AllowedRoles.Contains(role) && (await _userManager.IsInRoleAsync(user, role)))
                    await _userManager.RemoveFromRoleAsync(user, role);

            return true;
        }
    }
}
