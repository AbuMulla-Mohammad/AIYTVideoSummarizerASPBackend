
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class UpdateSummarizationRequestStatusCommandHandler : IRequestHandler<UpdateSummarizationRequestStatusCommand, SummarizationRequestDto>
    {
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly IMapper _mapper;

        public UpdateSummarizationRequestStatusCommandHandler(
            ISummarizationRequestRepository summarizationRequestRepository,
            IMapper mapper)
        {
            _summarizationRequestRepository = summarizationRequestRepository;
            _mapper = mapper;
        }

        public async Task<SummarizationRequestDto> Handle(UpdateSummarizationRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var summarizationRequest = await _summarizationRequestRepository.GetByIdAsync(request.RequestId);
            if (summarizationRequest == null)
                throw new KeyNotFoundException($"Request {request.RequestId} not found.");
            summarizationRequest.RequestStatus = request.RequestStatus;
            summarizationRequest.ErrorMessage = request.ErrorMessage;
            await _summarizationRequestRepository.UpdateAsync(summarizationRequest);

            var updatedRequest = await _summarizationRequestRepository.GetByIdAsync(request.RequestId);

            return _mapper.Map<SummarizationRequestDto>(updatedRequest);
        }
    }
}
