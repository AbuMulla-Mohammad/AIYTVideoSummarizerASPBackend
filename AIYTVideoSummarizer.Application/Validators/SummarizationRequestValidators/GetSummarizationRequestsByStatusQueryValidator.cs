
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
        }
    }
}
