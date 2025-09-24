
using AIYTVideoSummarizer.Application.Commands.UserCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class CreateExternalUserCommandValidator : AbstractValidator<CreateExternalUserCommand>
    {
        public CreateExternalUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long.")
                .MaximumLength(70).WithMessage("Username cannot exceed 70 characters.");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("A valid email address is required.")
               .MaximumLength(254).WithMessage("Email cannot exceed 254 characters.");


            RuleFor(ex => ex.LoginProvider)
                .NotEmpty().WithMessage("Login provider is required.")
                .MaximumLength(100).WithMessage("Login provider cannot exceed 100 characters.");

            RuleFor(ex => ex.ProviderKey)
                .NotEmpty().WithMessage("Provider key is required.")
                .MinimumLength(5).WithMessage("Provider key must be at least 5 characters.")
                .MaximumLength(200).WithMessage("Provider key cannot exceed 200 characters.");
        }
    }
}
