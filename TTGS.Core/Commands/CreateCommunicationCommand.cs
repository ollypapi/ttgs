using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Entity;
using TTGS.Shared.Request;

namespace TTGS.Core.Commands
{
    public class CreateCommunicationCommand : IRequest<bool>
    {
        public CreateCommunicationRequest Communication { get; set; }
        public bool IsCreatedByAdmin { get; set; }
        public bool Commit { get; set; }
    }

    public class CreateCommunicationCommandHandler : IRequestHandler<CreateCommunicationCommand, bool>
    {
        private readonly IEmailService _emailService;
        private readonly ITTGSUnitOfWork _unitOfWork;

        public CreateCommunicationCommandHandler(ITTGSUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreateCommunicationCommand request, CancellationToken cancellationToken)
        {
            var Communication = new Communication
            {
                UserId = Guid.Parse(request.Communication.UserId),
                RecipientId = Guid.Parse(request.Communication.RecipientId),
                Name = request.Communication.Name,
                Email = request.Communication.Email,
                Message = request.Communication.Message,
                Subject = request.Communication.Subject,
                IsCreatedByAdmin = request.IsCreatedByAdmin,
                DateCreated = DateTime.Now
            };

            await _unitOfWork.Communications.InsertAsync(Communication);
            if (request.Commit) await _unitOfWork.SaveAsync();

            var fullName = $"{Communication.Name}";
            await _emailService.SendEmailAsync("noreply@toyintrailers.com",
                $"Dear {Communication.Name}, <br /> {request.Communication.Message} TTGS Team",
                fullName,
                request.Communication.Subject,
                Communication.Email);

            return true;

        }
    }
}
