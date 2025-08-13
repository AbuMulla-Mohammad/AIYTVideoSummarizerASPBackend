
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class GetAllSummarizationRequestsQueryHandler : IRequestHandler<GetAllSummarizationRequestsQuery, List<SummarizationRequestDto>>
    {
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly IMapper _mapper;

        public GetAllSummarizationRequestsQueryHandler(
            ISummarizationRequestRepository summarizationRequestRepository,
            IMapper mapper)
        {
            _summarizationRequestRepository = summarizationRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<SummarizationRequestDto>> Handle(GetAllSummarizationRequestsQuery request, CancellationToken cancellationToken)
        {
            var summarizationRequests = await _summarizationRequestRepository.GetAllAsync();
            return _mapper.Map<List<SummarizationRequestDto>>(summarizationRequests);
        }
    }
}
