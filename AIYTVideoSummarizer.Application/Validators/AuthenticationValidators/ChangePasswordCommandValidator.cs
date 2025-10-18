using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.AuthenticationValidators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old password is required.")
                .MinimumLength(8).WithMessage("Old password must be at least 8 characters long.")
                .Must(password => password.Any(char.IsUpper) && password.Any(char.IsLower))
                .WithMessage("Old password must contain both uppercase and lowercase characters.")
                .Must(password => password.Any(char.IsDigit))
                .WithMessage("Old password must contain at least one digit.")
                .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch)))
                .WithMessage("Old password must contain at least one special character.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
                .Must(password => password.Any(char.IsUpper) && password.Any(char.IsLower))
                .WithMessage("New password must contain both uppercase and lowercase characters.")
                .Must(password => password.Any(char.IsDigit))
                .WithMessage("New password must contain at least one digit.")
                .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch)))
                .WithMessage("New password must contain at least one special character.");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(password => password.NewPassword)
                .WithMessage("Confirm password must match the new password.");
        }
    }
}
