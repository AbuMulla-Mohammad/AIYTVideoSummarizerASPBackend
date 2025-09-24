using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.VideoValidators
{
    public class GetVideoByIdQueryValidator : AbstractValidator<GetVideoByIdQuery>
    {
        public GetVideoByIdQueryValidator()
        {
            RuleFor(v => v.VideoId)
            .NotEmpty().WithMessage("Video Id must be provided.");
        }
    }
}
