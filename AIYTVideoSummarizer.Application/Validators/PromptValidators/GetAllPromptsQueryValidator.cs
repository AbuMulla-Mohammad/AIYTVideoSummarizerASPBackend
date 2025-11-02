using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.PromptValidators
{
    public class GetAllPromptsQueryValidator : AbstractValidator<GetAllPromptsQuery>
    {
        public GetAllPromptsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
