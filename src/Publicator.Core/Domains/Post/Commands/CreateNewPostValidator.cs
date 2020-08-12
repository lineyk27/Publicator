using System.Linq;
using FluentValidation;

namespace Publicator.Core.Domains.Post.Commands
{
    class CreateNewPostValidator : AbstractValidator<CreateNewPost>
    {
        public CreateNewPostValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(5, 75)
                .WithMessage("{PropertyName} length from {MinLength} to {MaxLength} characters");
            
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(10, 5000)
                .WithMessage("{PropertyName} length from {MinLength} to {MaxLength} characters");

            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.CommunityId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Tags).Must(x => x.Count() >= 1 && x.Count() <= 10);
            RuleForEach(x => x.Tags).NotEmpty().Length(1, 20);
        }
    }
}
