
using AIYTVideoSummarizer.Application.Queries.UserExternalLoginQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserExternalLoginValidators
{
    public class GetUserExternalLoginsQueryValidator : AbstractValidator<GetUserExternalLoginsQuery>
    {
        public GetUserExternalLoginsQueryValidator()
        {
            RuleFor(ex => ex.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
