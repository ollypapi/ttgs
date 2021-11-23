using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class SearchUserRequestValidator : AbstractValidator<SearchUserRequest>
    {
        public SearchUserRequestValidator()
        {
            RuleFor(m => m.searchParams).NotNull().Must(x=>x.Length >= 3).WithMessage("At least 3 characters must be supplied ");
        }
    }
}
