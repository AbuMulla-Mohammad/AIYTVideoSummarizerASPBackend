using AIYTVideoSummarizer.Application.Queries.UserQueries;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.UserValidators
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
