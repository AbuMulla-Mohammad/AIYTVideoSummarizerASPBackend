
using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.VideoValidators
{
    public class UpdateVideoCommandValidator : AbstractValidator<UpdateVideoCommand>
    {
        public UpdateVideoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Video Id must be provided.");

            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.")
                .When(v => !string.IsNullOrEmpty(v.Title));

            RuleFor(x => x.ChannelName)
                .MaximumLength(50).WithMessage("Channel Name cannot exceed 50 characters.")
                .When(v => !string.IsNullOrEmpty(v.ChannelName));
        }
    }
}
