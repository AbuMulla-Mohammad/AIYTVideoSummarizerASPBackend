
using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.PromptValidators
{
    public class GetPromptSummariesQueryValidator : AbstractValidator<GetPromptSummariesQuery>
    {
        public GetPromptSummariesQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Prompt Id must be provided.");
        }
    }
}
