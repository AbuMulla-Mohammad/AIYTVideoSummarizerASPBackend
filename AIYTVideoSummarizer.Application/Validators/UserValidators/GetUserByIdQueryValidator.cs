
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
