
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummarizationRequestValidators
{
    public class GetSummarizationRequestByIdQueryValidator : AbstractValidator<GetSummarizationRequestByIdQuery>
    {
        public GetSummarizationRequestByIdQueryValidator()
        {
            RuleFor(sr => sr.SummarizationRequestId)
            .NotEmpty().WithMessage("Summarization Request Id must be provided.");
        }
    }
}
