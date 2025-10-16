using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.AuthenticationValidators
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Verification token is required.")
                .Length(10, 500).WithMessage("Invalid verification token length.");
        }
    }
}
