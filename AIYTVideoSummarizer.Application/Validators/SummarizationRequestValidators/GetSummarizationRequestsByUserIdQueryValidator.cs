
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummarizationRequestValidators
{
    public class GetSummarizationRequestsByUserIdQueryValidator : AbstractValidator<GetSummarizationRequestsByUserIdQuery>
    {
        public GetSummarizationRequestsByUserIdQueryValidator()
        {
            RuleFor(sr => sr.UserId)
            .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
