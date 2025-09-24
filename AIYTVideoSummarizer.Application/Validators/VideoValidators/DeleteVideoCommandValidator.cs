
using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.VideoValidators
{
    public class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
    {
        public DeleteVideoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Video Id must be provided.");
        }
    }
}
