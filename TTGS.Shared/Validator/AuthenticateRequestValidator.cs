using FluentValidation;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(m => m.Username).EmailAddress().WithMessage("Username must be a valid email address");
            RuleFor(m => m.Password).Must(x => ConditionHelper.ValidatePassword(x)).WithMessage("Password must be longer than 6 characters");
        }
    }
}
