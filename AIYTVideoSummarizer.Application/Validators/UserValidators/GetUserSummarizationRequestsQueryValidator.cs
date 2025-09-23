
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class GetUserSummarizationRequestsQueryValidator : AbstractValidator<GetUserSummarizationRequestsQuery>
    {
        public GetUserSummarizationRequestsQueryValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
