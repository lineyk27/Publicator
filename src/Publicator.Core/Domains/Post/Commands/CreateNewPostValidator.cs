using FluentValidation;

namespace Publicator.Core.Domains.Post.Commands
{
    class CreateNewPostValidator : AbstractValidator<CreateNewPost>
    {
        public CreateNewPostValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .Length(5, 75)
                .WithMessage($"From {5} to {75} characters");
            
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required")
                .Length(10, 5000)
                .WithMessage($"From {10} to {5000} characters");

            RuleFor(x => x.UserId).NotEmpty().WithMessage("Creator user id is required");
            RuleFor(x => x.CommunityId).NotEmpty().WithMessage("Community id is required");
        }
    }
}
