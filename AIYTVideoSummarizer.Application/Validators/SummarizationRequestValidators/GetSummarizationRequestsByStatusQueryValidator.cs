
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummarizationRequestValidators
{
    public class GetSummarizationRequestsByStatusQueryValidator : AbstractValidator<GetSummarizationRequestsByStatusQuery>
    {
        public GetSummarizationRequestsByStatusQueryValidator()
        {
            RuleFor(sr => sr.RequestStatus)
               .IsInEnum().WithMessage("Invalid request status.");

            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
