
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.VideoValidators
{
    public class GetVideoSummaryByUrlPromptTypeQueryValidator : AbstractValidator<GetVideoSummaryByUrlPromptTypeQuery>
    {
        public GetVideoSummaryByUrlPromptTypeQueryValidator()
        {
            RuleFor(x => x.VideoUrl)
                .NotEmpty().WithMessage("YouTube video URL is required.")
                .MaximumLength(500).WithMessage("YouTube video URL can't be longer than 500 characters.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid URL format.")
                .Must(url => url.Contains("youtube.com") || url.Contains("youtu.be"))
                .WithMessage("URL must be a YouTube link.");

            RuleFor(x => x.PromptName)
                .NotEmpty().WithMessage("Prompt name must be provided.")
                .MaximumLength(80).WithMessage("Prompt name cannot exceed 80 characters.");
        }
    }
}
