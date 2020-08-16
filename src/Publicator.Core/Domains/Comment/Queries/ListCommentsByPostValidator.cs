using FluentValidation;

namespace Publicator.Core.Domains.Comment.Queries
{
    class ListCommentsByPostValidator : AbstractValidator<ListCommentsByPost>
    {
        public ListCommentsByPostValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().WithMessage("Post id is required");
        }
    }
}
