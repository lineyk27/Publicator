using FluentValidation;

namespace Publicator.Core.Domains.User.Commands
{
    class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Nickname).MinimumLength(4).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword)
                .Equal(y => y.ConfirmPassword)
                .WithMessage("Password and confirm password are not equals");
        }
    }
}
