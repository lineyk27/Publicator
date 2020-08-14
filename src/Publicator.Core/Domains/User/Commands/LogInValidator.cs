using System;
using System.Collections.Generic;
using FluentValidation;

namespace Publicator.Core.Domains.User.Commands
{
    class LogInValidator : AbstractValidator<LogIn>
    {
        public LogInValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("{PropertyName} must be email adress or nickname");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("{PropertyName} min length {MinLength} characters is required");
        }
    }
}
