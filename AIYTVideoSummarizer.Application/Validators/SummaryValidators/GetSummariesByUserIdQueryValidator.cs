
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummaryValidators
{
    public class GetSummariesByUserIdQueryValidator:AbstractValidator<GetSummariesByUserIdQuery>
    {
        public GetSummariesByUserIdQueryValidator()
        {
            RuleFor(s => s.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");

            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
