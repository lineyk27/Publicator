using FluentValidation;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsBySubscriptionValidator : AbstractValidator<ListPostsBySubscription>
    {
        public ListPostsBySubscriptionValidator()
        {
            RuleFor(x => x.Page).NotEmpty().WithMessage("Page number is required.");
            RuleFor(x => x.PageSize).NotEmpty().WithMessage("Page size is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Current user id is required.");
        }
    }
}
