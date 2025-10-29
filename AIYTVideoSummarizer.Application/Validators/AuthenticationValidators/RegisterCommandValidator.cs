
using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.AuthenticationValidators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("First Name cannot exceed 70 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(1).WithMessage("Last Name must be at least 1 characters long.")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 70 characters.");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("A valid email address is required.")
               .MaximumLength(254).WithMessage("Email cannot exceed 254 characters.");

            RuleFor(x => x.Password)
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
