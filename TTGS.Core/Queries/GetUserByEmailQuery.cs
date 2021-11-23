using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Entity;
using TTGS.Shared.Request;

namespace TTGS.Core.Queries
{

    public class GetUserByEmailQuery : IRequest<Unit>
    {
        public ForgotPasswordRequest Parameters { get; set; }
        public string ResetUrl { get; set; }
    }
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Unit>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IEmailService _emailService;
        public GetUserByEmailQueryHandler(ITTGSUnitOfWork unitOfWork, UserManager<AspNetUsers> userManager, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
        }


        public async Task<Unit> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.AspNetUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == request.Parameters.UserRegisteredEmail.ToLower());
            if (user != null)
            {
                var generateToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetMessage = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(request.ResetUrl + "&code=" + generateToken)}'>clicking here</a>";
        
               await  _emailService.SendEmailAsync("noreply@toyintrailers.com",
                $"Dear {user.FirstName}, <br /> {resetMessage} . <br /> Regards, <br /> TTGS Team",
                "",
                "TTGS Password Password Recovery",
                user.Email);
            }
            return Unit.Value;
        }
    }
}
