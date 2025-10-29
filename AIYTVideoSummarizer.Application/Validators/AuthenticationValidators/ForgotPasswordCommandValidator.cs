using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.AuthenticationValidators
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("A valid email address is required.")
               .MaximumLength(254).WithMessage("Email cannot exceed 254 characters.");
        }
    }
}
