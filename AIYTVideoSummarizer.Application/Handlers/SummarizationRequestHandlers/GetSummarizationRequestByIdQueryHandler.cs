
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class GetSummarizationRequestByIdQueryHandler : IRequestHandler<GetSummarizationRequestByIdQuery, SummarizationRequestDetailsDto>
    {
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly IMapper _mapper;

        public GetSummarizationRequestByIdQueryHandler(ISummarizationRequestRepository summarizationRequestRepository,
            IMapper mapper)
        {
            _summarizationRequestRepository = summarizationRequestRepository;
            _mapper = mapper;
        }
        public async Task<SummarizationRequestDetailsDto> Handle(GetSummarizationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var summarizationRequest = await _summarizationRequestRepository.GetByIdAsync(request.SummarizationRequestId, sr => sr.Prompt, sr => sr.Video)
                ?? throw new NotFoundException(nameof(SummarizationRequest), request.SummarizationRequestId);
            return _mapper.Map<SummarizationRequestDetailsDto>(summarizationRequest);
        }
    }
}
