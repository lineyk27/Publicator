using System;
using System.Collections.Generic;
using FluentValidation;

namespace Publicator.Core.Domains.Vote.Commands
{
    class VoteForPostValidator : AbstractValidator<VoteForPost>
    {
        public VoteForPostValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().WithMessage("{PeopertyName} is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PeopertyName} is required");
            RuleFor(x => x.Up).NotEmpty().WithMessage("{PeopertyName} is required");
        }
    }
}
