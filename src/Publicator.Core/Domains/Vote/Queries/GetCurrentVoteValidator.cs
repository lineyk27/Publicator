using FluentValidation;

namespace Publicator.Core.Domains.Vote.Queries
{
    class GetCurrentVoteValidator : AbstractValidator<GetCurrentVote>
    {
        public GetCurrentVoteValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.PostId).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
