using FluentValidation;

namespace Publicator.Core.Domains.User.Queries
{
    class GetByUsernameValidator : AbstractValidator<GetByUsername>
    {
        public GetByUsernameValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
