
using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.PromptValidators
{
    public class CreatePromptCommandValidator : AbstractValidator<CreatePromptCommand>
    {
        public CreatePromptCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(80).WithMessage("Name must not exceed 80 characters.");

            RuleFor(p => p.Text)
                .NotEmpty().WithMessage("Text is required.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
