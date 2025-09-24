
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class GetUserSummariesQueryValidator : AbstractValidator<GetUserSummariesQuery>
    {
        public GetUserSummariesQueryValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
