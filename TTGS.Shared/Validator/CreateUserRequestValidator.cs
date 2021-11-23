using FluentValidation;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(m => m.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(m => m.Email).EmailAddress().WithMessage("Email must be a valid email address");
            RuleFor(m => m.Password).Must(x => ConditionHelper.ValidatePassword(x)).WithMessage("Password must be longer than 6 characters"); 
            RuleFor(m => m.Roles).Must(x => x.Count > 0).WithMessage("User must have at least one role assigned");
        }
    }
}
