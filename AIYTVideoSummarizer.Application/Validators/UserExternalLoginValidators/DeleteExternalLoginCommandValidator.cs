
using AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserExternalLoginValidators
{
    public class DeleteExternalLoginCommandValidator : AbstractValidator<DeleteExternalLoginCommand>
    {
        public DeleteExternalLoginCommandValidator()
        {
            RuleFor(ex=>ex.Id)
                .NotEmpty().WithMessage("External Login Id must be provided.");
        }
    }
}
