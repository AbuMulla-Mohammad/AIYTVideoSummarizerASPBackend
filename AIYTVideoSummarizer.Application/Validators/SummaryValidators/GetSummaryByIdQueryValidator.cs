
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummaryValidators
{
    public class GetSummaryByIdQueryValidator : AbstractValidator<GetSummaryByIdQuery>
    {
        public GetSummaryByIdQueryValidator()
        {
            RuleFor(s=>s.SummaryId)
                .NotEmpty().WithMessage("Summary Id must be provided.");
        }
    }
}
