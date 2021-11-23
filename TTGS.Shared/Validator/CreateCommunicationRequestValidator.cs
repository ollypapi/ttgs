using FluentValidation;
using System;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class CreateCommunicationRequestValidator : AbstractValidator<CreateCommunicationRequest>
    {
        public CreateCommunicationRequestValidator()
        {
            RuleFor(m => m.RecipientId).Must(x => Guid.TryParse(x, out Guid value)).WithMessage("Recipient must be a valid GUID");
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(m => m.Email).EmailAddress().WithMessage("Email must be a valid email address");
            RuleFor(m => m.Message).NotEmpty().WithMessage("Message is required");
        }
    }
}
