
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
        }
    }
}
