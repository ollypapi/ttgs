using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.Enums;
using TTGS.Shared.Request;

namespace TTGS.Shared.Validator
{
    public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
    {
        public UpdateOrderStatusRequestValidator()
        {
            RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("Invalid status");
        }
    }
}
