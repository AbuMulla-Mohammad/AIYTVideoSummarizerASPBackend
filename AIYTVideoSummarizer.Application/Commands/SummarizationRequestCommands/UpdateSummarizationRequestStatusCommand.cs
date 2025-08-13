
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Enums;
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands
{
    public class UpdateSummarizationRequestStatusCommand:IRequest<SummarizationRequestDto>
    {
        public Guid RequestId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
