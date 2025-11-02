
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

            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
