using FluentValidation;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(m => m.Roles).Must(x => x.Count > 0).WithMessage("User must have at least one role assigned");
        }
    }
}
