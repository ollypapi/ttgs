using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(m => m.NewPassword).Must(x => ConditionHelper.ValidatePassword(x)).WithMessage("Password must be longer than 6 characters");
            RuleFor(m => m.Token).NotEmpty().WithMessage("Reset token cannot be null");
        }
    }
}
