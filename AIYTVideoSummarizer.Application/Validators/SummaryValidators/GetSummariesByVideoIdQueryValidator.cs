using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummaryValidators
{
    public class GetSummariesByVideoIdQueryValidator : AbstractValidator<GetSummariesByVideoIdQuery>
    {
        public GetSummariesByVideoIdQueryValidator()
        {
            RuleFor(s => s.VideoId)
                .NotEmpty().WithMessage("Video Id must be provided.");
        }
    }
}
