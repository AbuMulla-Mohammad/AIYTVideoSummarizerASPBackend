
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Domain.Enums;
using FluentValidation;

namespace AIYTVideoSummarizer.Application.Validators.SummarizationRequestValidators
{
    public class UpdateSummarizationRequestStatusCommandValidator : AbstractValidator<UpdateSummarizationRequestStatusCommand>
    {
        public UpdateSummarizationRequestStatusCommandValidator()
        {
            RuleFor(sr => sr.RequestId)
                .NotEmpty().WithMessage("RequestId is required.");

            RuleFor(sr => sr.RequestStatus)
                .IsInEnum().WithMessage("Invalid request status.");

            RuleFor(sr => sr.ErrorMessage)
                .NotEmpty()
                .When(sr => sr.RequestStatus == RequestStatus.Failed)
                .WithMessage("Error message is required when the status is Failed.");
        }
    }
}
