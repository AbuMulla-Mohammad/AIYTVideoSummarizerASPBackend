
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummaryValidators
{
    public class GetSummariesByPromptIdQueryValidator : AbstractValidator<GetSummariesByPromptIdQuery>
    {
        public GetSummariesByPromptIdQueryValidator()
        {
            RuleFor(s => s.PromptId)
                .NotEmpty().WithMessage("Prompt Id must be provided.");

            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
