
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummarizationRequestValidators
{
    public class CreateSummarizationRequestCommandValidator : AbstractValidator<CreateSummarizationRequestCommand>
    {
        public CreateSummarizationRequestCommandValidator()
        {
            RuleFor(sr => sr.YouTubeUrl)
                .NotEmpty().WithMessage("YouTube video URL is required.")
                .MaximumLength(500).WithMessage("YouTube video URL can't be longer than 500 characters.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid URL format.")
                .Must(url => url.Contains("youtube.com") || url.Contains("youtu.be"))
                .WithMessage("URL must be a YouTube link.");

            RuleFor(sr => sr.PromptId)
                .NotEmpty().WithMessage("Prompt Id must be provided.");

            RuleFor(sr => sr.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");

        }
    }
}
