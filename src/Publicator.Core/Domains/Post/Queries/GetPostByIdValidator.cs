using FluentValidation;

namespace Publicator.Core.Domains.Post.Queries
{
    class GetPostByIdValidator : AbstractValidator<GetPostById>
    {
        public GetPostByIdValidator()
        {
            RuleFor(x => x.PostId).NotNull().NotEmpty().WithMessage("Post's id is required");
        }
    }
}
