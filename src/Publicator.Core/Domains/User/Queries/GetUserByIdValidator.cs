using System;
using System.Collections.Generic;
using FluentValidation;

namespace Publicator.Core.Domains.User.Queries
{
    class GetUserByIdValidator : AbstractValidator<GetUserById>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
