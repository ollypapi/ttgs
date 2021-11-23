using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Entity;
using TTGS.Shared.Request;

namespace TTGS.Core.Commands
{
    public class UpdateUserPasswordCommand : IRequest<bool>
    {
        public ResetPasswordRequest ResetPasswordRequest { get; set; }
        public bool Commit { get; set; }
    }
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, bool>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUsers> _userManager;
        public UpdateUserPasswordCommandHandler(ITTGSUnitOfWork unitOfWork,
            UserManager<AspNetUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<bool> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.AspNetUsers.AsQueryable().FirstOrDefault(x => x.Id == request.ResetPasswordRequest.Id);
            if (user != null)
            {
                await _userManager.ResetPasswordAsync(user,request.ResetPasswordRequest.Token,request.ResetPasswordRequest.NewPassword);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.ResetPasswordRequest.NewPassword);

                _unitOfWork.AspNetUsers.Update(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
