using FluentValidation;

namespace Publicator.Core.Domains.User.Commands
{
    class ConfirmAccountRegistrationValidator : AbstractValidator<ConfirmAccountRegistration>
    {
        public ConfirmAccountRegistrationValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ConfirmationToken).NotEmpty();
        }
    }
}
