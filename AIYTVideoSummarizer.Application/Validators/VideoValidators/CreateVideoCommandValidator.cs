
using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.VideoValidators
{
    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {
            RuleFor(v => v.YouTubeVideoID)
                .NotEmpty().WithMessage("YouTube video ID is required.")
                .MaximumLength(11).WithMessage("YouTube video ID must be 11 characters long.");

            RuleFor(v => v.YouTubeUrl)
                .NotEmpty().WithMessage("YouTube video URL is required.")
                .MaximumLength(500).WithMessage("YouTube video URL can't be longer than 500 characters.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid URL format.")
                .Must(url => url.Contains("youtube.com") || url.Contains("youtu.be"))
                .WithMessage("URL must be a YouTube link.");


            RuleFor(v => v.Title)
                .MaximumLength(100).WithMessage("Title can't be longer than 100 characters.")
                .When(v => !string.IsNullOrEmpty(v.Title));

            RuleFor(v => v.ChannelName)
                .MaximumLength(50).WithMessage("Channel Name can't be longer than 50 characters.")
                .When(v => !string.IsNullOrEmpty(v.ChannelName));
        }
    }
}
