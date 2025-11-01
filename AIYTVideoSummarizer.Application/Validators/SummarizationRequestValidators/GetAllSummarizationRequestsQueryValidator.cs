using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummarizationRequestValidators
{
    public class GetAllSummarizationRequestsQueryValidator : AbstractValidator<GetAllSummarizationRequestsQuery>
    {
        public GetAllSummarizationRequestsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
