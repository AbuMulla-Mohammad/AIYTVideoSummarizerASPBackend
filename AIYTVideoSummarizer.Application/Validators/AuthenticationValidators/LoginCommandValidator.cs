using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.AuthenticationValidators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("A valid email address is required.")
               .MaximumLength(254).WithMessage("Email cannot exceed 254 characters.");

            RuleFor(u => u.Password)
              .NotEmpty().WithMessage("Password is required.")
              .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
              .Must(password => password.Any(char.IsUpper) && password.Any(char.IsLower))
              .WithMessage("Password must contain both uppercase and lowercase characters.")
              .Must(password => password.Any(char.IsDigit))
              .WithMessage("Password must contain at least one digit.")
              .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch)))
              .WithMessage("Password must contain at least one special character.");
        }
    }
}
