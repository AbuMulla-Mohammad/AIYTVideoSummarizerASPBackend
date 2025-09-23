
using AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserExternalLoginValidators
{
    public class CreateExternalLoginCommandValidator : AbstractValidator<CreateExternalLoginCommand>
    {
        public CreateExternalLoginCommandValidator()
        {
            RuleFor(ex => ex.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");

            RuleFor(ex=>ex.LoginProvider)
                .NotEmpty().WithMessage("Login provider is required.")
                .MaximumLength(100).WithMessage("Login provider cannot exceed 100 characters.");

            RuleFor(ex=>ex.ProviderKey)
                .NotEmpty().WithMessage("Provider key is required.")
                .MinimumLength(5).WithMessage("Provider key must be at least 5 characters.")
                .MaximumLength(200).WithMessage("Provider key cannot exceed 200 characters.");
        }
    }
}
