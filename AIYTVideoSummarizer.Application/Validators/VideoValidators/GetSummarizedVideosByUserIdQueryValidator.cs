
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
            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
