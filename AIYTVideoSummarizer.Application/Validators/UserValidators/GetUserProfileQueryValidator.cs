
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
    {
        public GetUserProfileQueryValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
