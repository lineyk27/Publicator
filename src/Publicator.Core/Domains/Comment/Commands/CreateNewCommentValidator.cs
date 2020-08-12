using System;
using System.Collections.Generic;
using FluentValidation;

namespace Publicator.Core.Domains.Comment.Commands
{
    class CreateNewCommentValidator : AbstractValidator<CreateNewComment>
    {
        public CreateNewCommentValidator()
        {
            RuleFor(x => x.Content).NotEmpty().MinimumLength(1).MaximumLength(1000);
            RuleFor(x => x.PostId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
