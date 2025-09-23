
using AIYTVideoSummarizer.Application.Commands.UserCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long.")
                .MaximumLength(70).WithMessage("Username cannot exceed 70 characters.");

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
