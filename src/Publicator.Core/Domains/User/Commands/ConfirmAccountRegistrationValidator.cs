using FluentValidation;

namespace Publicator.Core.Domains.User.Commands
{
    class ConfirmAccountRegistrationValidator : AbstractValidator<ConfirmAccountRegistration>
    {
        public ConfirmAccountRegistrationValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
