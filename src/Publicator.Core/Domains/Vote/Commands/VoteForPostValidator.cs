using System;
using System.Collections.Generic;
using FluentValidation;

namespace Publicator.Core.Domains.Vote.Commands
{
    class VoteForPostValidator : AbstractValidator<VoteForPost>
    {
        public VoteForPostValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Up).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
