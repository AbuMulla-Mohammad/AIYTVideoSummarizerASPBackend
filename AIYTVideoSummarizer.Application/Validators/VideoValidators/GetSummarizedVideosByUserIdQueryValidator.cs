
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.VideoValidators
{
    public class GetSummarizedVideosByUserIdQueryValidator : AbstractValidator<GetSummarizedVideosByUserIdQuery>
    {
        public GetSummarizedVideosByUserIdQueryValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
