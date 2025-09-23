
using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.PromptValidators
{
    public class GetPromptByIdQueryValidator : AbstractValidator<GetPromptByIdQuery>
    {
        public GetPromptByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Prompt Id must be provided.");
        }
    }
}
