
using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.PromptValidators
{
    public class UpdatePromptCommandValidator : AbstractValidator<UpdatePromptCommand>
    {
        public UpdatePromptCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Prompt Id is required.");

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Name cannot be empty if provided.")
               .MaximumLength(80).WithMessage("Name must not exceed 80 characters.")
               .When(p => p.Name != null);

            RuleFor(p => p.Text)
                .NotEmpty().WithMessage("Text cannot be empty if provided.")
                .When(p => p.Text != null);

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description cannot be empty if provided.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(p => p.Description != null);
        }
    }
}
