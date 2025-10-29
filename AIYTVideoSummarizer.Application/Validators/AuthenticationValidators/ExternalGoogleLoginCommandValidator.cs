using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.AuthenticationValidators
{
    public class ExternalGoogleLoginCommandValidator : AbstractValidator<ExternalGoogleLoginCommand>
    {
        public ExternalGoogleLoginCommandValidator()
        {
            RuleFor(x => x.IdToken)
             .NotEmpty().WithMessage("Google ID token is required.");
        }
    }
}
