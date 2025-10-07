
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
        }
    }
}
