using MediatR;
using TTGS.Core.Interfaces;
using TTGS.Shared.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Shared.Request;
using Microsoft.AspNetCore.Identity;
using TTGS.Shared.Constants;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TTGS.Core.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public CreateUserRequest User { get; set; }
        public bool Commit { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<AspNetUsers> _userManager;
        private ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
            IEmailService emailService,
            UserManager<AspNetUsers> userManager)
        {
            _logger = logger;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AspNetUsers
            {
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Email = request.User.Email,
                UserName = request.User.Email,
                PhoneNumber = request.User.CellPhone
            };

            var result = await _userManager.CreateAsync(user, request.User.Password);
            if (result.Succeeded)
            {
                foreach (var role in request.User.Roles)
                    if (UserRoleConstants.AllowedRoles.Contains(role))
                        await _userManager.AddToRoleAsync(user, role);


                var fullName = $"{user.FirstName} {user.LastName}";
                await _emailService.SendEmailAsync("noreply@toyintrailers.com",
                    $"Dear {user.FirstName}, <br /> We are pleased to inform you that your account has been created. <br /> Regards, <br /> TTGS Team",
                    fullName,
                    "TTGS Account Created",
                    user.Email);

                return true;
            }

            _logger.Log(LogLevel.Error, $"Failed to create user. Reason: {JsonConvert.SerializeObject(result)}");
            throw new Exception("Failed to create user");
        }
    }
}
