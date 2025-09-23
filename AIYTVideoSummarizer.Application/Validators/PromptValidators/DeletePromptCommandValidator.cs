using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.PromptValidators
{
    public class DeletePromptCommandValidator : AbstractValidator<DeletePromptCommand>
    {
        public DeletePromptCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Prompt Id must be provided.");
        }
    }
}
