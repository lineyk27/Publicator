using FluentValidation;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsByCreatorUserValidator : AbstractValidator<ListPostsByCreatorUser>
    {
        public ListPostsByCreatorUserValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
