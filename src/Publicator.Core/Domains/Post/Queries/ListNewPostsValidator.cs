﻿using FluentValidation;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListNewPostsValidator : AbstractValidator<ListNewPosts>
    {
        public ListNewPostsValidator()
        {
            RuleFor(x => x.Page).NotEmpty().WithMessage("Page number is required").GreaterThan(0);
            RuleFor(x => x.PageSize).NotEmpty().WithMessage("Page size is required").GreaterThan(0);
        }
    }
}
