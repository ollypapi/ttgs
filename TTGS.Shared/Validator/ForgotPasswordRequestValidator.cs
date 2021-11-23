using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(s => s.UserRegisteredEmail).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required");
        }
    }
}
