using FluentValidation;

namespace Publicator.Core.Domains.User.Commands
{
    class FacebookLogInValidator : AbstractValidator<FacebookLogIn>
    {
        public FacebookLogInValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
        }
    }
}
